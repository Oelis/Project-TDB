using System;
using Abilities.Effects;
using Enums;

namespace Interfaces
{
    public interface IDamageable
    {
        void TakeDamage(int damage, Type sourceEffectType, StatType ResistanceStat, bool canBeEvaded, bool canBeBlocked);
        
        void Heal(int amount);

        void Die();

        bool ApplyEffect(EffectOverTime effect);
        
        void RemoveEffect(EffectOverTime effect);

        public event Action OnTurnStart;
        public event Action OnTurnEnd;

    }
}