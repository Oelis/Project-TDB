using System;
using Abilities.Effects;
using Enums;

namespace Interfaces
{
    public interface IDamageable
    {
        void TakeDamage(float damage, DamageType type);
        
        void Heal(float amount);

        void Die();

        bool ApplyEffect(EffectOverTime effect);
        
        void RemoveEffect(EffectOverTime effect);

        public event Action OnTurnStart;
        public event Action OnTurnEnd;

    }
}