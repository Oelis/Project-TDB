using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Abilities;
using Abilities.AbilityEffects;
using Attributes;
using Enums;
using Interfaces;
using Units.Logic;

namespace Units
{
    [Serializable]
    public abstract class UnitBrain : IDamageable
    {
        public event Action OnTurnStart;
        public event Action OnTurnEnd;
        
        private readonly ImmunityLogic immunityLogic = new ImmunityLogic();
        
        private readonly DotLogic dotLogic = new DotLogic();
        
        private readonly StatModifLogic statModifLogic = new StatModifLogic();
        
        private readonly StatusLogic statusLogic = new StatusLogic();
        
        protected AbilityManager abilityManager;
        
        protected Unit source;
        
        public Stats.Stats Stats {get; protected set; } 
        
        private float currentHealth;
        
        public virtual void TakeDamage(float damage, DamageType damageType)
        {
            if (immunityLogic.IsDamageImmuneTo(damageType)) return;
            // Check if evaded
            // Check if blocked
            // Calculate final damage output
            currentHealth -= damage;
            
        }
        
        public void Die()
        {
            var activeDot = dotLogic.GetDamageOvertime();
            foreach (var effect in activeDot)
            {
                effect.OnCompleted -= RemoveEffect;
                effect.Cleanup();
            }
            
            var activeImmunities = immunityLogic.GetImmunityEffects();
            foreach (var immunities in activeImmunities)
            {
                immunities.OnCompleted -= RemoveEffect;
                immunities.Cleanup();
            }
            source.Kill();
        }
        
        public void ApplyEffect(EffectOverTime effect)
        {
            if(immunityLogic.BlockEffect(effect)) return;
            
            effect.OnCompleted += RemoveEffect;
            
            switch (effect)
            {
                case ImmunityEffect immunity:
                    immunityLogic.AddImmunityEffect(immunity);
                    break;
                case DamageOverTimeEffect damageOverTime:
                    dotLogic.AddDamageOvertime(damageOverTime);
                    break;
            }
        }
        
        public void RemoveEffect(EffectOverTime effect)
        {
            effect.OnCompleted -= RemoveEffect;

            switch (effect)
            {
                case ImmunityEffect immunity:
                    immunityLogic.RemoveImmunityEffect(immunity);
                    break;
                case DamageOverTimeEffect damageOverTime:
                    dotLogic.RemoveDamageOvertime(damageOverTime);
                    break;
            }
        }
    }
    
    
}