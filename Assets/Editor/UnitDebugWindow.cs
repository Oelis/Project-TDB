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

public class UnitDebugWindow : OdinEditorWindow
{
    // --- Cached reflection ------------------------------------------------
    private static readonly FieldInfo UnitBrainField        = typeof(Unit).GetField("_Brain",                 BindingFlags.NonPublic | BindingFlags.Instance);
    private static readonly FieldInfo BrainHealthField      = typeof(UnitBrain).GetField("currentHealth",     BindingFlags.NonPublic | BindingFlags.Instance);
    private static readonly FieldInfo BrainAbilityCtrlField = typeof(UnitBrain).GetField("AbilityController", BindingFlags.NonPublic | BindingFlags.Instance);
    private static readonly FieldInfo BrainDotLogicField    = typeof(UnitBrain).GetField("dotLogic",          BindingFlags.NonPublic | BindingFlags.Instance);
    private static readonly FieldInfo BrainImmunityField    = typeof(UnitBrain).GetField("immunityLogic",     BindingFlags.NonPublic | BindingFlags.Instance);
    private static readonly FieldInfo StatsConfigField      = typeof(Stats.Stats).GetField("_statConfig",     BindingFlags.NonPublic | BindingFlags.Instance);
    private static readonly FieldInfo ActiveAbilitiesField  = typeof(AbilityController).GetField("_activeAbilities",  BindingFlags.NonPublic | BindingFlags.Instance);
    private static readonly FieldInfo PassiveAbilitiesField = typeof(AbilityController).GetField("_passiveAbilities", BindingFlags.NonPublic | BindingFlags.Instance);
    private static readonly FieldInfo DotEffectsField       = typeof(DotLogic).GetField("_doteffects",        BindingFlags.NonPublic | BindingFlags.Instance);
    private static readonly FieldInfo ImmunityEffectsField  = typeof(ImmunityLogic).GetField("_immunityeffects", BindingFlags.NonPublic | BindingFlags.Instance);

    // --- Window -----------------------------------------------------------
    [MenuItem("Tools/Unit Debug Window")]
    private static void OpenWindow() => GetWindow<UnitDebugWindow>("Unit Debug").Show();

    // Backing list hidden; each unit is drawn via its own PropertyTree.
    [HideInInspector]
    private List<UnitSnapshot> _units = new List<UnitSnapshot>();
    private readonly List<PropertyTree> _unitTrees = new List<PropertyTree>();

    // --- Toolbar ----------------------------------------------------------
    [HorizontalGroup("Toolbar"), ToggleLeft, LabelText("Auto Refresh"), LabelWidth(90), PropertyOrder(-2)]
    public bool AutoRefresh = true;

    [HorizontalGroup("Toolbar"), SuffixLabel("seconds", overlay: true), LabelText("Interval"), LabelWidth(55),
     MinValue(0.1f), MaxValue(10f), PropertyOrder(-2)]
    public float RefreshInterval = 0.5f;

    [HorizontalGroup("Toolbar"), Button("Refresh Now"), GUIColor(0.4f, 0.85f, 0.4f), PropertyOrder(-2)]
    private void Refresh()
    {
        var currentUnits = FindObjectsByType<Unit>(FindObjectsSortMode.None);

        // Full rebuild only when unit roster changes
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

        // Roster unchanged: update mutable runtime data in-place (no tree recreation)
        for (int i = 0; i < currentUnits.Length; i++)
            UpdateRuntimeData(_units[i], currentUnits[i]);
    }

    private static void UpdateRuntimeData(UnitSnapshot snap, Unit unit)
    {
        var brain = UnitBrainField?.GetValue(unit) as UnitBrain;
        if (brain == null) return;

        snap.CurrentHealth = (float)(BrainHealthField?.GetValue(brain) ?? 0f);

        if (brain.Stats != null)
        {
            var config = StatsConfigField?.GetValue(brain.Stats) as UnitConfig;
            snap.Stats = new StatsSnapshot(config, brain.Stats);
        }

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

    // --- Draw units without any list wrapper ------------------------------
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

    // --- Auto-refresh -----------------------------------------------------
    private double _lastRefreshTime;

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

    private void Update()
    {
        if (!AutoRefresh || !Application.isPlaying) return;
        if (EditorApplication.timeSinceStartup - _lastRefreshTime < RefreshInterval) return;
        _lastRefreshTime = EditorApplication.timeSinceStartup;
        Refresh();
        Repaint();
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

        if (brain.Stats != null)
        {
            var config = StatsConfigField?.GetValue(brain.Stats) as UnitConfig;
            snap.Stats = new StatsSnapshot(config, brain.Stats);
        }

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

    // Abilities - backing lists hidden, drawn via cached PropertyTrees
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

    // DoTs
    [HideInInspector] public List<DotSnapshot> ActiveDots = new List<DotSnapshot>();
    private List<PropertyTree> _dotTrees = new List<PropertyTree>();

    [FoldoutGroup("DoT Effects"), OnInspectorGUI]
    private void DrawDots()
    {
        if (_dotTrees.Count == 0) { EditorGUILayout.LabelField("None", EditorStyles.centeredGreyMiniLabel); return; }
        foreach (var tree in _dotTrees) { tree.Draw(false); GUILayout.Space(2); }
    }

    // Immunities
    [HideInInspector] public List<ImmunitySnapshot> ActiveImmunities = new List<ImmunitySnapshot>();
    private List<PropertyTree> _immunityTrees = new List<PropertyTree>();

    [FoldoutGroup("Immunities"), OnInspectorGUI]
    private void DrawImmunities()
    {
        if (_immunityTrees.Count == 0) { EditorGUILayout.LabelField("None", EditorStyles.centeredGreyMiniLabel); return; }
        foreach (var tree in _immunityTrees) { tree.Draw(false); GUILayout.Space(2); }
    }

    // Called once when the unit is first discovered
    public void BuildInnerTrees()
    {
        _activeTrees   = ActiveAbilities.Select(a => PropertyTree.Create(a)).ToList();
        _passiveTrees  = PassiveAbilities.Select(a => PropertyTree.Create(a)).ToList();
        _dotTrees      = ActiveDots.Select(d => PropertyTree.Create(d)).ToList();
        _immunityTrees = ActiveImmunities.Select(i => PropertyTree.Create(i)).ToList();
    }

    // Called on every tick — mutates existing snapshot objects so their PropertyTrees
    // never need to be recreated. Trees are only rebuilt when the count changes.
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
    public StatsSnapshot() { }

    public StatsSnapshot(UnitConfig config, Stats.Stats live)
    {
        if (config == null) return;
        Attack         = config.attack;
        Defense        = config.defense;
        Intelligence   = live?.Intelligence ?? config.intelligence;
        Dexterity      = config.dexterity;
        Strength       = live?.Strength    ?? config.strength;
        Constitution   = config.constitution;
        Speed          = config.speed;
        CriticalChance           = live?.CriChance ?? config.criticalChance;
        CriticalDamageMultiplier = config.criticalDamageMultiplier;
        EvadeRate                = config.evadeRate;
        BlockRate                = config.blockRate;
        PhysicalResist  = config.physicalResist;
        FireResist      = config.fireResist;
        IceResist       = config.iceResist;
        PoisonResist    = config.poisonResist;
        LightningResist = config.lightningResist;
    }

    [BoxGroup("Primary Stats")]
    [HorizontalGroup("Primary Stats/Row1"), ReadOnly, LabelWidth(100)] public int Attack;
    [HorizontalGroup("Primary Stats/Row1"), ReadOnly, LabelWidth(100)] public int Defense;
    [HorizontalGroup("Primary Stats/Row1"), ReadOnly, LabelWidth(100)] public int Speed;
    [HorizontalGroup("Primary Stats/Row2"), ReadOnly, LabelWidth(100)] public int Strength;
    [HorizontalGroup("Primary Stats/Row2"), ReadOnly, LabelWidth(100)] public int Intelligence;
    [HorizontalGroup("Primary Stats/Row2"), ReadOnly, LabelWidth(100)] public int Dexterity;
    [HorizontalGroup("Primary Stats/Row3"), ReadOnly, LabelWidth(100)] public int Constitution;

    [BoxGroup("Combat Stats")]
    [HorizontalGroup("Combat Stats/Row1"), ReadOnly, LabelWidth(150)] public float CriticalChance;
    [HorizontalGroup("Combat Stats/Row1"), ReadOnly, LabelWidth(150)] public float CriticalDamageMultiplier;
    [HorizontalGroup("Combat Stats/Row2"), ReadOnly, LabelWidth(150)] public float EvadeRate;
    [HorizontalGroup("Combat Stats/Row2"), ReadOnly, LabelWidth(150)] public float BlockRate;

    [BoxGroup("Resistances")]
    [HorizontalGroup("Resistances/Row1"), ReadOnly, LabelWidth(130)] public int PhysicalResist;
    [HorizontalGroup("Resistances/Row1"), ReadOnly, LabelWidth(130)] public int FireResist;
    [HorizontalGroup("Resistances/Row1"), ReadOnly, LabelWidth(130)] public int IceResist;
    [HorizontalGroup("Resistances/Row2"), ReadOnly, LabelWidth(130)] public int PoisonResist;
    [HorizontalGroup("Resistances/Row2"), ReadOnly, LabelWidth(130)] public int LightningResist;
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
    [HorizontalGroup("Row"), ReadOnly, LabelWidth(110)] public int DamagePerTurn;
    [HorizontalGroup("Row"), ReadOnly, LabelWidth(110), Tooltip("-1 = infinite")] public int TurnsRemaining;
    [HorizontalGroup("Row"), ReadOnly, LabelWidth(110)] public bool CanBeCleansed;

    public DotSnapshot(DamageOverTimeEffect effect)
    {
        EffectType     = FormatTypeName(effect.GetType().Name);
        DamagePerTurn  = effect.damagePerTurn;
        TurnsRemaining = effect._turnDuration;
        CanBeCleansed  = effect.CanBeCleanse;
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
        ImmuneToEffects = ImmunityLogic.GetTypeImmunityInfo(type)?.Select(t => FormatTypeName(t.Name)).ToList() ?? new List<string>();
    }

    private static string FormatTypeName(string raw) =>
        System.Text.RegularExpressions.Regex.Replace(raw, "(?<!^)([A-Z])", " $1");
}
