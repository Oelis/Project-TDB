using System;
using Enums;
using Interfaces;
using Units;

namespace Abilities.AbilityEffects
{
    [Serializable]
    public class DamageEffect : IEffect<IDamageable>
    {
        public int damageAmount = 10;
        public DamageType damageType;
        
        public event Action<IEffect<IDamageable>> OnCompleted;
        public void Apply(Unit source, IDamageable target)
        {
            //if(CritPolicy.GetOrCreate().Roll(source.Stats.CriChance)) damageAmount = damageAmount * source.Stats.Strength;
            target.TakeDamage(damageAmount,damageType);
            OnCompleted?.Invoke(this);
        }
        
        public void Cleanup()
        {
        }
        
    }
}