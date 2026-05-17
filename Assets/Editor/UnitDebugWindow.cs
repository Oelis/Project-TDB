using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Abilities;
using Abilities.Effects;
using Enums;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using Units;
using Units.Logic;
using UnityEditor;
using UnityEngine;
using ObjectFieldAlignment = Sirenix.OdinInspector.ObjectFieldAlignment;

namespace Editor
{
    public class UnitDebugWindow : OdinEditorWindow
    {
        // --- Cached reflection ------------------------------------------------
        private static readonly FieldInfo UnitBrainField           = typeof(Unit).GetField("_Brain",                    BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly FieldInfo BrainHealthField         = typeof(UnitBrain).GetField("currentHealth",        BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly FieldInfo BrainAbilityCtrlField    = typeof(UnitBrain).GetField("AbilityController",    BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly FieldInfo BrainDotLogicField       = typeof(UnitBrain).GetField("dotLogic",             BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly FieldInfo BrainImmunityField       = typeof(UnitBrain).GetField("immunityLogic",        BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly FieldInfo BrainStatModifLogicField = typeof(UnitBrain).GetField("statModifLogic",       BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly FieldInfo ActiveAbilitiesField     = typeof(AbilityController).GetField("_activeAbilities",  BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly FieldInfo PassiveAbilitiesField    = typeof(AbilityController).GetField("_passiveAbilities", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly FieldInfo DotEffectsField          = typeof(DotLogic).GetField("_doteffects",           BindingFlags.NonPublic | BindingFlags.Instance);
        internal static readonly FieldInfo DotDamagePerTurnField   = typeof(DamageOverTimeEffect).GetField("damagePerTurn", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly FieldInfo ImmunityEffectsField     = typeof(ImmunityLogic).GetField("_immunityeffects", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly FieldInfo StatsModifModifiersField = typeof(Stats.StatsModifLogic).GetField("_modifiers",   BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly FieldInfo StatsStatConfigField     = typeof(Stats.Stats).GetField("_statConfig",        BindingFlags.NonPublic | BindingFlags.Instance);

        // --- Window -----------------------------------------------------------
        [MenuItem("Tools/Unit Debug Window")]
        private static void OpenWindow() => GetWindow<UnitDebugWindow>("Unit Debug").Show();

        [HideInInspector]
        private List<UnitSnapshot> _units = new List<UnitSnapshot>();
        private readonly List<PropertyTree> _unitTrees = new List<PropertyTree>();

        // --- Toolbar ----------------------------------------------------------
        [HorizontalGroup("Toolbar"), Button("Refresh Now"), GUIColor(0.4f, 0.85f, 0.4f), PropertyOrder(-2)]
        private void Refresh()
        {
            var currentUnits = FindObjectsByType<Unit>(FindObjectsSortMode.None);

            bool rosterChanged = currentUnits.Length != _units.Count;
            if (!rosterChanged)
            {
                for (int i = 0; i < currentUnits.Length; i++)
                {
                    if (_units[i].UnitName != currentUnits[i].name) { rosterChanged = true; break; }
                }
            }

            if (rosterChanged)
            {
                DisposeUnitTrees();
                _units.Clear();
                foreach (var unit in currentUnits)
                {
                    var snap = BuildSnapshot(unit);
                    _units.Add(snap);
                    _unitTrees.Add(PropertyTree.Create(snap));
                }
                return;
            }

            for (int i = 0; i < currentUnits.Length; i++)
                UpdateRuntimeData(_units[i], currentUnits[i]);
        }

        private static void UpdateRuntimeData(UnitSnapshot snap, Unit unit)
        {
            var brain = UnitBrainField?.GetValue(unit) as UnitBrain;
            if (brain == null) return;

            snap.CurrentHealth = (float)(BrainHealthField?.GetValue(brain) ?? 0f);
            snap.Stats = BuildStatsSnapshot(brain);

            var dotLogic = BrainDotLogicField?.GetValue(brain) as DotLogic;
            if (dotLogic != null)
            {
                var newDots = (DotEffectsField?.GetValue(dotLogic) as List<DamageOverTimeEffect>)
                    ?.Select(d => new DotSnapshot(d)).ToList() ?? new List<DotSnapshot>();
                snap.UpdateDots(newDots);
            }

            var immunityLogic = BrainImmunityField?.GetValue(brain) as ImmunityLogic;
            if (immunityLogic != null)
            {
                var newImmunities = (ImmunityEffectsField?.GetValue(immunityLogic) as List<ImmunityEffect>)
                    ?.Select(i => new ImmunitySnapshot(i)).ToList() ?? new List<ImmunitySnapshot>();
                snap.UpdateImmunities(newImmunities);
            }
        }

        // --- Draw units -------------------------------------------------------
        [OnInspectorGUI, PropertyOrder(0)]
        private void DrawUnits()
        {
            if (_unitTrees.Count == 0)
            {
                SirenixEditorGUI.InfoMessageBox("No units found. Enter Play Mode or press Refresh Now.");
                return;
            }
            foreach (var tree in _unitTrees)
            {
                SirenixEditorGUI.DrawThickHorizontalSeparator(2, 4);
                tree.Draw(applyUndo: false);
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            if (Application.isPlaying) Refresh();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            DisposeUnitTrees();
        }

        private void DisposeUnitTrees()
        {
            foreach (var t in _unitTrees) t.Dispose();
            _unitTrees.Clear();
            foreach (var s in _units) s.Dispose();
        }

        // --- Snapshot builder -------------------------------------------------
        private static UnitSnapshot BuildSnapshot(Unit unit)
        {
            var snap = new UnitSnapshot { UnitName = unit.name };

            var brain = UnitBrainField?.GetValue(unit) as UnitBrain;
            if (brain == null) return snap;

            snap.BrainType     = brain.GetType().Name;
            snap.CurrentHealth = (float)(BrainHealthField?.GetValue(brain) ?? 0f);
            snap.Stats         = BuildStatsSnapshot(brain);

            var ctrl = BrainAbilityCtrlField?.GetValue(brain) as AbilityController;
            if (ctrl != null)
            {
                snap.ActiveAbilities  = (ActiveAbilitiesField?.GetValue(ctrl)  as List<ActiveAbility>)?.Select(a => new AbilitySnapshot(a)).ToList()  ?? new List<AbilitySnapshot>();
                snap.PassiveAbilities = (PassiveAbilitiesField?.GetValue(ctrl) as List<PassiveAbility>)?.Select(a => new AbilitySnapshot(a)).ToList() ?? new List<AbilitySnapshot>();
            }

            var dotLogic = BrainDotLogicField?.GetValue(brain) as DotLogic;
            if (dotLogic != null)
                snap.ActiveDots = (DotEffectsField?.GetValue(dotLogic) as List<DamageOverTimeEffect>)?.Select(d => new DotSnapshot(d)).ToList() ?? new List<DotSnapshot>();

            var immunityLogic = BrainImmunityField?.GetValue(brain) as ImmunityLogic;
            if (immunityLogic != null)
                snap.ActiveImmunities = (ImmunityEffectsField?.GetValue(immunityLogic) as List<ImmunityEffect>)?.Select(i => new ImmunitySnapshot(i)).ToList() ?? new List<ImmunitySnapshot>();

            snap.BuildInnerTrees();
            return snap;
        }

        // --- Stat breakdown ---------------------------------------------------
        private static StatsSnapshot BuildStatsSnapshot(UnitBrain brain)
        {
            if (brain.Stats == null) return new StatsSnapshot();

            var modifLogic = BrainStatModifLogicField?.GetValue(brain) as Stats.StatsModifLogic;
            var modifiers  = (modifLogic != null ? StatsModifModifiersField?.GetValue(modifLogic) as LinkedList<StatModifierEffect> : null)
                             ?? new LinkedList<StatModifierEffect>();

            var config = StatsStatConfigField?.GetValue(brain.Stats) as UnitConfig;

            var snap = new StatsSnapshot
            {
                PrimaryStats = new[] { StatType.Attack, StatType.Defense, StatType.Speed, StatType.Strength, StatType.Intelligence, StatType.Dexterity, StatType.Constitution }
                    .Select(t => BuildStatEntry(t, config, modifiers)).ToList(),

                CombatStats = new[] { StatType.CriticalChance, StatType.CriticalDamageMultiplier, StatType.EvadeRate, StatType.BlockRate }
                    .Select(t => BuildStatEntry(t, config, modifiers)).ToList(),

                Resistances = new[] { StatType.PhysicalResist, StatType.FireResist, StatType.IceResist, StatType.PoisonResist, StatType.LightningResist, StatType.BleedResist }
                    .Select(t => BuildStatEntry(t, config, modifiers)).ToList()
            };

            return snap;
        }

        private static StatDebugEntry BuildStatEntry(StatType statType, UnitConfig config, LinkedList<StatModifierEffect> modifiers)
        {
            int baseValue = config != null ? GetBaseValue(config, statType) : 0;
            var query     = new Query(statType, baseValue);
            var steps     = new List<StatModifierStep>();

            foreach (var modifier in modifiers)
            {
                int before = query.Value;
                modifier.Handle(query);
                if (query.Value != before)
                    steps.Add(new StatModifierStep { ModifierType = modifier.GetType().Name, ValueAfter = query.Value });
            }

            return new StatDebugEntry { StatName = statType.ToString(), Base = baseValue, Final = query.Value, Modifiers = steps };
        }

        private static int GetBaseValue(UnitConfig config, StatType statType) => statType switch
        {
            StatType.Attack                   => config.attack,
            StatType.Defense                  => config.defense,
            StatType.Intelligence             => config.intelligence,
            StatType.Dexterity                => config.dexterity,
            StatType.Strength                 => config.strength,
            StatType.Constitution             => config.constitution,
            StatType.Speed                    => config.speed,
            StatType.CriticalChance           => config.criticalChance,
            StatType.CriticalDamageMultiplier => config.criticalDamageMultiplier,
            StatType.EvadeRate                => config.evadeRate,
            StatType.BlockRate                => config.blockRate,
            StatType.PhysicalResist           => config.physicalResist,
            StatType.FireResist               => config.fireResist,
            StatType.IceResist                => config.iceResist,
            StatType.PoisonResist             => config.poisonResist,
            StatType.LightningResist          => config.lightningResist,
            StatType.BleedResist              => config.bleedResist,
            _                                 => 0
        };
    }

// ===========================================================================
//  Unit Snapshot
// ===========================================================================
    [Serializable]
    public class UnitSnapshot : IDisposable
    {
        [HideInInspector] public string UnitName  = "Unknown";
        [HideInInspector] public string BrainType = "None";

        [ShowInInspector, ReadOnly, HideLabel, DisplayAsString, GUIColor(1f, 0.85f, 0.3f), PropertyOrder(-10)]
        private string _header => UnitName + "  [" + BrainType + "]";

        [ShowInInspector, ReadOnly, LabelText("Current HP"), PropertyOrder(-9)]
        public float CurrentHealth;

        [FoldoutGroup("Stats"), ShowInInspector, ReadOnly, HideLabel, InlineProperty]
        public StatsSnapshot Stats = new StatsSnapshot();

        [HideInInspector] public List<AbilitySnapshot> ActiveAbilities  = new List<AbilitySnapshot>();
        [HideInInspector] public List<AbilitySnapshot> PassiveAbilities = new List<AbilitySnapshot>();
        private List<PropertyTree> _activeTrees  = new List<PropertyTree>();
        private List<PropertyTree> _passiveTrees = new List<PropertyTree>();

        [TabGroup("Abilities", "Active"), OnInspectorGUI]
        private void DrawActiveAbilities()
        {
            if (_activeTrees.Count == 0) { EditorGUILayout.LabelField("None", EditorStyles.centeredGreyMiniLabel); return; }
            foreach (var tree in _activeTrees) { tree.Draw(false); GUILayout.Space(2); }
        }

        [TabGroup("Abilities", "Passive"), OnInspectorGUI]
        private void DrawPassiveAbilities()
        {
            if (_passiveTrees.Count == 0) { EditorGUILayout.LabelField("None", EditorStyles.centeredGreyMiniLabel); return; }
            foreach (var tree in _passiveTrees) { tree.Draw(false); GUILayout.Space(2); }
        }

        [HideInInspector] public List<DotSnapshot> ActiveDots = new List<DotSnapshot>();
        private List<PropertyTree> _dotTrees = new List<PropertyTree>();

        [FoldoutGroup("DoT Effects"), OnInspectorGUI]
        private void DrawDots()
        {
            if (_dotTrees.Count == 0) { EditorGUILayout.LabelField("None", EditorStyles.centeredGreyMiniLabel); return; }
            foreach (var tree in _dotTrees) { tree.Draw(false); GUILayout.Space(2); }
        }

        [HideInInspector] public List<ImmunitySnapshot> ActiveImmunities = new List<ImmunitySnapshot>();
        private List<PropertyTree> _immunityTrees = new List<PropertyTree>();

        [FoldoutGroup("Immunities"), OnInspectorGUI]
        private void DrawImmunities()
        {
            if (_immunityTrees.Count == 0) { EditorGUILayout.LabelField("None", EditorStyles.centeredGreyMiniLabel); return; }
            foreach (var tree in _immunityTrees) { tree.Draw(false); GUILayout.Space(2); }
        }

        public void BuildInnerTrees()
        {
            _activeTrees   = ActiveAbilities.Select(a => PropertyTree.Create(a)).ToList();
            _passiveTrees  = PassiveAbilities.Select(a => PropertyTree.Create(a)).ToList();
            _dotTrees      = ActiveDots.Select(d => PropertyTree.Create(d)).ToList();
            _immunityTrees = ActiveImmunities.Select(i => PropertyTree.Create(i)).ToList();
        }

        public void UpdateDots(List<DotSnapshot> incoming)
        {
            if (incoming.Count != ActiveDots.Count)
            {
                foreach (var t in _dotTrees) t.Dispose();
                ActiveDots = incoming;
                _dotTrees  = ActiveDots.Select(d => PropertyTree.Create(d)).ToList();
                return;
            }
            for (int i = 0; i < incoming.Count; i++)
            {
                ActiveDots[i].TurnsRemaining = incoming[i].TurnsRemaining;
                ActiveDots[i].DamagePerTurn  = incoming[i].DamagePerTurn;
            }
        }

        public void UpdateImmunities(List<ImmunitySnapshot> incoming)
        {
            if (incoming.Count != ActiveImmunities.Count)
            {
                foreach (var t in _immunityTrees) t.Dispose();
                ActiveImmunities = incoming;
                _immunityTrees   = ActiveImmunities.Select(i => PropertyTree.Create(i)).ToList();
                return;
            }
            for (int i = 0; i < incoming.Count; i++)
                ActiveImmunities[i].TurnsRemaining = incoming[i].TurnsRemaining;
        }

        public void Dispose()
        {
            foreach (var t in _activeTrees)   t.Dispose();
            foreach (var t in _passiveTrees)  t.Dispose();
            foreach (var t in _dotTrees)      t.Dispose();
            foreach (var t in _immunityTrees) t.Dispose();
        }
    }

// ===========================================================================
//  Stats Snapshot
// ===========================================================================
    [Serializable]
    public class StatsSnapshot
    {
        [BoxGroup("Primary Stats"), ListDrawerSettings(IsReadOnly = true, ShowFoldout = false)]
        public List<StatDebugEntry> PrimaryStats = new List<StatDebugEntry>();

        [BoxGroup("Combat Stats"), ListDrawerSettings(IsReadOnly = true, ShowFoldout = false)]
        public List<StatDebugEntry> CombatStats = new List<StatDebugEntry>();

        [BoxGroup("Resistances"), ListDrawerSettings(IsReadOnly = true, ShowFoldout = false)]
        public List<StatDebugEntry> Resistances = new List<StatDebugEntry>();
    }

// ===========================================================================
//  Stat Debug Entry
// ===========================================================================
    [Serializable, InlineProperty]
    public class StatDebugEntry
    {
        [HorizontalGroup("H"), ReadOnly, HideLabel]
        public string StatName;

        [HorizontalGroup("H"), ReadOnly, LabelWidth(55)]
        public int Base;

        [HorizontalGroup("H"), ReadOnly, LabelWidth(55)]
        public int Final;

        [HideIf("HasNoModifiers")]
        [ListDrawerSettings(IsReadOnly = true, ShowFoldout = true)]
        public List<StatModifierStep> Modifiers = new List<StatModifierStep>();

        private bool HasNoModifiers() => Modifiers == null || Modifiers.Count == 0;
    }

// ===========================================================================
//  Stat Modifier Step
// ===========================================================================
    [Serializable]
    public class StatModifierStep
    {
        [HorizontalGroup, ReadOnly, LabelWidth(220), GUIColor(1f, 0.75f, 0.4f)]
        public string ModifierType;

        [HorizontalGroup, ReadOnly, LabelWidth(80)]
        public int ValueAfter;
    }

// ===========================================================================
//  Ability Snapshot
// ===========================================================================
    [Serializable]
    public class AbilitySnapshot
    {
        [HideInInspector] public string Name;

        [HorizontalGroup("Row"), PreviewField(50, ObjectFieldAlignment.Left), ReadOnly, HideLabel, LabelWidth(55)]
        public Texture Icon;

        [HorizontalGroup("Row"), VerticalGroup("Row/Info"), ReadOnly, HideLabel, DisplayAsString, GUIColor(1f, 0.85f, 0.3f)]
        public string AbilityName;

        [VerticalGroup("Row/Info"), ReadOnly, HideLabel, DisplayAsString, Multiline(2)]
        public string Description;

        [VerticalGroup("Row/Info"), ReadOnly, LabelText("Effects")]
        [ListDrawerSettings(IsReadOnly = true, ShowFoldout = true)]
        public List<string> EffectEntries = new List<string>();

        public AbilitySnapshot(Ability ability)
        {
            Name          = ability.abilityName;
            AbilityName   = ability.abilityName;
            Description   = ability.description;
            Icon          = ability.icon;
            EffectEntries = ability.effectEntries?.Select(e => e.Label).ToList() ?? new List<string>();
        }
    }

// ===========================================================================
//  DoT Snapshot
// ===========================================================================
    [Serializable]
    public class DotSnapshot
    {
        [HorizontalGroup("Row"), ReadOnly, LabelWidth(110), GUIColor(1f, 0.5f, 0.3f)] public string EffectType;
        [HorizontalGroup("Row"), ReadOnly, LabelWidth(110)] public string DamageType;
        [HorizontalGroup("Row"), ReadOnly, LabelWidth(110)] public int DamagePerTurn;
        [HorizontalGroup("Row"), ReadOnly, LabelWidth(110), Tooltip("-1 = infinite")] public int TurnsRemaining;
        [HorizontalGroup("Row"), ReadOnly, LabelWidth(110)] public bool CanBeCleansed;

        public DotSnapshot(DamageOverTimeEffect effect)
        {
            EffectType     = FormatTypeName(effect.GetType().Name);
            DamagePerTurn  = (int)(UnitDebugWindow.DotDamagePerTurnField?.GetValue(effect) ?? 0);
            TurnsRemaining = effect._turnDuration;
            CanBeCleansed  = effect.CanBeCleanse;
            DamageType     = effect.DamageType.ToString();
        }

        private static string FormatTypeName(string raw) =>
            System.Text.RegularExpressions.Regex.Replace(raw, "(?<!^)([A-Z])", " $1");
    }

// ===========================================================================
//  Immunity Snapshot
// ===========================================================================
    [Serializable]
    public class ImmunitySnapshot
    {
        [HorizontalGroup("Row"), ReadOnly, LabelWidth(140), GUIColor(0.4f, 0.9f, 1f)] public string EffectType;
        [HorizontalGroup("Row"), ReadOnly, LabelWidth(140), Tooltip("-1 = infinite")] public int TurnsRemaining;

        [ReadOnly, LabelText("Immune to")]
        [ListDrawerSettings(IsReadOnly = true, ShowFoldout = false)]
        public List<string> ImmuneToEffects = new List<string>();

        public ImmunitySnapshot(ImmunityEffect effect)
        {
            var type       = effect.GetType();
            EffectType     = FormatTypeName(type.Name);
            TurnsRemaining = effect._turnDuration;

            var (damageTypes, eotTypes) = ImmunityLogic.GetImmunityInfo(type);
            ImmuneToEffects = damageTypes.Select(d => d.ToString())
                .Concat(eotTypes.Select(e => "All " + e + "s"))
                .ToList();
        }

        private static string FormatTypeName(string raw) =>
            System.Text.RegularExpressions.Regex.Replace(raw, "(?<!^)([A-Z])", " $1");
    }
}