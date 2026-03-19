using System;
using Enums;
using Interfaces;
using Units;

namespace Abilities.AbilityEffects
{
    [Serializable]
    public class DamageEffect : IEffect
    {
        public int damageAmount = 10;
        public DamageType damageType;
        public void Apply(UnitBrain source, UnitBrain target)
        {
            //if(CritPolicy.GetOrCreate().Roll(source.Stats.CriChance)) damageAmount = damageAmount * source.Stats.Strength;
            target.TakeDamage(damageAmount,damageType);
        }
        
        public void Cleanup()
        {
        }
        
    }
}