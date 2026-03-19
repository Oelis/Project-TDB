using System;
using Enums;
using Interfaces;
using Units;

namespace Abilities.AbilityEffects
{
    [Serializable]
    public abstract class DamageOverTimeEffect :EffectOverTime, IDebuff
    {
        public int damagePerTurn;
        
        public abstract DamageType DamageType { get; }
        
        public override void Tick()
        {
            CurrentTarget.TakeDamage(damagePerTurn, DamageType);
        }
        
        
    }
}