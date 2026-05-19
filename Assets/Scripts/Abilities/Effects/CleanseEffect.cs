using System;
using System.Collections.Generic;
using Enums;
using Interfaces;
using Sirenix.OdinInspector;
using Units;
using UnityEngine;

namespace Abilities.Effects
{
    [Serializable]
    public class CleanseEffect : IEffect
    {
        [SerializeField] private CleanseMode mode;

        [SerializeField, ShowIf(nameof(NeedsAmount)), MinValue(1)]
        private int amount = 1;

        [SerializeField, ShowIf(nameof(NeedsEffectType))]
        [ValueDropdown(nameof(GetEffectTypeNames))]
        private string effectTypeFullName;

        [SerializeField, ShowIf(nameof(NeedsDamageTypeFilter))]
        [ValueDropdown(nameof(GetAllowedDotDamageTypes))]
        private DamageType dotDamageTypeFilter;

        private bool NeedsAmount() =>
            mode is CleanseMode.XType ||
            mode == CleanseMode.XBuff ||
            mode == CleanseMode.XDebuff;

        private bool NeedsEffectType() =>
            mode == CleanseMode.AllOfType ||
            mode == CleanseMode.XType;

        private bool NeedsDamageTypeFilter() =>
            NeedsEffectType() &&
            !string.IsNullOrEmpty(effectTypeFullName) &&
            Type.GetType(effectTypeFullName) == typeof(DamageOverTimeEffect);

        private IEnumerable<ValueDropdownItem<string>> GetEffectTypeNames()
        {
            var result = new List<ValueDropdownItem<string>>();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                Type[] types;
                try { types = assembly.GetTypes(); }
                catch { continue; }

                foreach (var t in types)
                    if (!t.IsAbstract && typeof(EffectOverTime).IsAssignableFrom(t))
                        result.Add(new ValueDropdownItem<string>(t.Name, t.AssemblyQualifiedName));
            }
            return result;
        }

        private IEnumerable<DamageType> GetAllowedDotDamageTypes()
        {
            foreach (DamageType d in Enum.GetValues(typeof(DamageType)))
                if (d != DamageType.PhysicalDamage) yield return d;
        }

        public void Apply(UnitBrain source, UnitBrain target)
        {
            var effects = target.GetAllCleanseableEffects();

            switch (mode)
            {
                case CleanseMode.AllOfType:
                    CleanseAllOfType(effects);
                    break;
                case CleanseMode.XType:
                    CleanseXOfType(effects, amount);
                    break;
                case CleanseMode.AllBuffs:
                    CleanseByEOTType(effects, EOTType.Buff);
                    break;
                case CleanseMode.AllDebuffs:
                    CleanseByEOTType(effects, EOTType.Debuff);
                    break;
                case CleanseMode.XBuff:
                    CleanseRandomByEOTType(effects, EOTType.Buff, amount);
                    break;
                case CleanseMode.XDebuff:
                    CleanseRandomByEOTType(effects, EOTType.Debuff, amount);
                    break;
            }
        }

        

        private void CleanseAllOfType(List<EffectOverTime> effects)
        {
            var toClean = new List<EffectOverTime>();
            for (int i = 0; i < effects.Count; i++)
                if (MatchesType(effects[i])) toClean.Add(effects[i]);

            for (int i = 0; i < toClean.Count; i++)
            {
                Debug.Log($"[CleanseEffect] Cleansed {toClean[i].GetType().Name}");
                toClean[i].Cleanup();
            }
        }

        private void CleanseXOfType(List<EffectOverTime> effects, int count)
        {
            var toClean = new List<EffectOverTime>();
            for (int i = 0; i < effects.Count && toClean.Count < count; i++)
                if (MatchesType(effects[i])) toClean.Add(effects[i]);

            for (int i = 0; i < toClean.Count; i++)
            {
                Debug.Log($"[CleanseEffect] Cleansed {toClean[i].GetType().Name}");
                toClean[i].Cleanup();
            }
        }

        private void CleanseByEOTType(List<EffectOverTime> effects, EOTType eotType)
        {
            var toClean = new List<EffectOverTime>();
            for (int i = 0; i < effects.Count; i++)
                if (effects[i].EOTType == eotType) toClean.Add(effects[i]);

            for (int i = 0; i < toClean.Count; i++)
            {
                Debug.Log($"[CleanseEffect] Cleansed {toClean[i].GetType().Name}");
                toClean[i].Cleanup();
            }
        }

        private void CleanseRandomByEOTType(List<EffectOverTime> effects, EOTType eotType, int count)
        {
            var filtered = new List<EffectOverTime>();
            for (int i = 0; i < effects.Count; i++)
                if (effects[i].EOTType == eotType) filtered.Add(effects[i]);

            Shuffle(filtered);
            int cleansed = 0;
            for (int i = 0; i < filtered.Count && cleansed < count; i++)
            {
                Debug.Log($"[CleanseEffect] Cleansed {filtered[i].GetType().Name}");
                filtered[i].Cleanup();
                cleansed++;
            }
        }

        private bool MatchesType(EffectOverTime effect)
        {
            var targetType = Type.GetType(effectTypeFullName);
            if (targetType == null) return false;

            if (targetType == typeof(DamageOverTimeEffect))
                return effect is DamageOverTimeEffect dot && dot.DamageType == dotDamageTypeFilter;

            return effect.GetType() == targetType;
        }

        private static void Shuffle(List<EffectOverTime> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = UnityEngine.Random.Range(0, i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }

        public void Cleanup() { }
    }
}
