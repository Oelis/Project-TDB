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
    public class FlipEffect : IEffect
    {
        [SerializeField] private FlipMode mode;

        [SerializeField, ShowIf(nameof(NeedsAmount)), MinValue(1)]
        private int amount = 1;

        [SerializeField, ShowIf(nameof(NeedsEffectType))]
        [ValueDropdown(nameof(GetFlippableEffectTypeNames))]
        private string effectTypeFullName;

        [SerializeField, ShowIf(nameof(NeedsDamageTypeFilter))]
        [ValueDropdown(nameof(GetAllowedDotDamageTypes))]
        private DamageType dotDamageTypeFilter;

        private bool NeedsAmount() => mode is FlipMode.RandomX or FlipMode.XType;

        private bool NeedsEffectType() => mode is FlipMode.AllOfType or FlipMode.XType;

        private bool NeedsDamageTypeFilter() =>
            NeedsEffectType() &&
            !string.IsNullOrEmpty(effectTypeFullName) &&
            Type.GetType(effectTypeFullName) == typeof(DamageOverTimeEffect);

        private IEnumerable<ValueDropdownItem<string>> GetFlippableEffectTypeNames()
        {
            var result = new List<ValueDropdownItem<string>>();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                Type[] types;
                try { types = assembly.GetTypes(); }
                catch { continue; }

                foreach (var t in types)
                    if (!t.IsAbstract && typeof(EffectOverTime).IsAssignableFrom(t) && typeof(IFlippable).IsAssignableFrom(t))
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
            var effects = target.GetAllFlippableEffects();

            switch (mode)
            {
                case FlipMode.RandomX:
                    FlipRandom(effects, source, target, amount);
                    break;
                case FlipMode.AllOfType:
                    FlipAllOfType(effects, source, target);
                    break;
                case FlipMode.XType:
                    FlipXOfType(effects, source, target, amount);
                    break;
                case FlipMode.AllBuffs:
                    FlipByEOTType(effects, source, target, EOTType.Buff);
                    break;
                case FlipMode.AllDebuffs:
                    FlipByEOTType(effects, source, target, EOTType.Debuff);
                    break;
            }
        }

        private void FlipRandom(List<EffectOverTime> effects, UnitBrain source, UnitBrain target, int count)
        {
            Shuffle(effects);
            int flipped = 0;
            for (int i = 0; i < effects.Count && flipped < count; i++)
            {
                DoFlip(effects[i], source, target);
                flipped++;
            }
        }

        private void FlipAllOfType(List<EffectOverTime> effects, UnitBrain source, UnitBrain target)
        {
            var toFlip = new List<EffectOverTime>();
            for (int i = 0; i < effects.Count; i++)
                if (MatchesType(effects[i])) toFlip.Add(effects[i]);

            for (int i = 0; i < toFlip.Count; i++)
                DoFlip(toFlip[i], source, target);
        }

        private void FlipXOfType(List<EffectOverTime> effects, UnitBrain source, UnitBrain target, int count)
        {
            var toFlip = new List<EffectOverTime>();
            for (int i = 0; i < effects.Count && toFlip.Count < count; i++)
                if (MatchesType(effects[i])) toFlip.Add(effects[i]);

            for (int i = 0; i < toFlip.Count; i++)
                DoFlip(toFlip[i], source, target);
        }

        private void FlipByEOTType(List<EffectOverTime> effects, UnitBrain source, UnitBrain target, EOTType eotType)
        {
            var toFlip = new List<EffectOverTime>();
            for (int i = 0; i < effects.Count; i++)
                if (effects[i].EOTType == eotType) toFlip.Add(effects[i]);

            for (int i = 0; i < toFlip.Count; i++)
                DoFlip(toFlip[i], source, target);
        }

        private void DoFlip(EffectOverTime effect, UnitBrain source, UnitBrain target)
        {
            if (effect.EOTType == EOTType.Buff   && target.HasSafeguardBuff()) return;
            if (effect.EOTType == EOTType.Debuff && target.HasTraumaDebuff())  return;
            EffectOverTime flipped = ((IFlippable)effect).Flip();
            Debug.Log($"[FlipEffect] {effect.GetType().Name} -> {flipped.GetType().Name}");
            effect.Cleanup();
            flipped.Apply(source, target);
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
