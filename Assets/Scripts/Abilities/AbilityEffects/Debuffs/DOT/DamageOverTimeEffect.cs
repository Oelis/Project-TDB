using System;
using Enums;
using Interfaces;
using Units;

namespace Abilities.AbilityEffects.Debuffs.DOT
{
    [Serializable]
    public abstract class DamageOverTimeEffect :Debuff, IEffect<IDamageable>
    {
        public int damagePerTurn;
        
        public abstract DamageType DamageType { get; }
        
        public override void Apply(Unit source,IDamageable target)
        {
            base.Apply(source, target);
            // Calculate damage per turn with stats modifier
            CurrentTarget.OnTurnStart+=Tick;
        }
        public override void Cleanup()
        {
            CurrentTarget.OnTurnStart-=Tick;
            base.Cleanup();
        }

        public virtual void Tick()
        {
            CurrentTarget.TakeDamage(damagePerTurn, DamageType);
        }
        
        
    }
}