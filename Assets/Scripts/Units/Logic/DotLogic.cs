using System.Collections.Generic;
using Abilities.AbilityEffects;

namespace Units.Logic
{
    public class DotLogic
    {
        private List<DamageOverTimeEffect> _doteffects = new();

        public List<DamageOverTimeEffect> GetDamageOvertime()
        {
            return _doteffects;
        }

        public void AddDamageOvertime(DamageOverTimeEffect effect)
        {
            _doteffects.Add(effect);
        }

        public void RemoveDamageOvertime(DamageOverTimeEffect effect)
        {
            _doteffects.Remove(effect);
        }
    }
}