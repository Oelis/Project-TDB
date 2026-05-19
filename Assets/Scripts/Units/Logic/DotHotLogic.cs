using System.Collections.Generic;
using Abilities.Effects;

namespace Units.Logic
{
    public class DotHotLogic
    {
        private readonly List<DamageOverTimeEffect> _dotEffects = new();
        private readonly List<HealOverTime> _hotEffects = new();

        public List<DamageOverTimeEffect> GetDamageOvertime() => _dotEffects;
        public List<HealOverTime> GetHealOverTime() => _hotEffects;

        public void AddDamageOvertime(DamageOverTimeEffect effect)  => _dotEffects.Add(effect);
        public void RemoveDamageOvertime(DamageOverTimeEffect effect) => _dotEffects.Remove(effect);

        public void AddHealOverTime(HealOverTime effect)    => _hotEffects.Add(effect);
        public void RemoveHealOverTime(HealOverTime effect) => _hotEffects.Remove(effect);
    }
}