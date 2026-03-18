using System;
using Abilities.AbilityEffects;
using Enums;

namespace Interfaces
{
    public interface IDamageable
    {
        void TakeDamage(float damage, DamageType type);

        void Die();

        void ApplyEffect(EffectOverTime effect);
        
        bool CanApplyEffect(EffectOverTime effect);
        
        bool IsDamageImmuneTo(DamageType type);
    
        void RemoveEffect(EffectOverTime effect);

        public event Action OnTurnStart;
        public event Action OnTurnEnd;

    }
}