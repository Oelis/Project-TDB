using System;
using Abilities.Effects;
using Enums;

namespace Interfaces
{
    public interface IDamageable
    {
        void TakeDamage(int damage, DamageType sourceEffectDamageType, bool canBeEvaded, bool canBeBlocked);
        
        void Heal(int amount);

        void Die();

        bool ApplyEffect(EffectOverTime effect);
        
        void RemoveEffect(EffectOverTime effect);

        public event Action OnCritDealt;
        
        public event Action OnCritMiss;

        public event Action OnCritReceive;

        public event Action OnBlock;

        public event Action OnBlockMiss;

        public event Action OnDamage;
        public event Action OnDamageReceive;
        
    }
}