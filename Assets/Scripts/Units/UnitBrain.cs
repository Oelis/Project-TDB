using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Abilities;
using Abilities.Effects;
using Attributes;
using Enums;
using Interfaces;
using NUnit.Framework;
using Stats;
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
        
        protected AbilityController AbilityController;
        
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

        public void Heal(float amount)
        {
            currentHealth += amount;
        }

        public virtual void Die()
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

        public AbilityController GetAbilityController()
        {
            return AbilityController;
        }
        
    }

    public abstract class UnitBrain<TSelf,TConfig> : UnitBrain where TSelf : UnitBrain<TSelf, TConfig> where TConfig : UnitConfig
    {
        protected TConfig config;
        
        public TSelf WithSource(Unit source) { this.source = source; return (TSelf)this; }                                                                                
        public TSelf WithAbilityManager() { this.AbilityController = new AbilityController(this); return (TSelf)this; }                                                         
        public TSelf WithConfig(TConfig config) { this.config = config; return (TSelf)this; }                                                                             
        public TSelf WithStats(Stats.Stats stats) { this.Stats = new Stats.Stats(new StatsMediator(), config); return (TSelf)this; } 
        public TSelf Clone() => (TSelf)MemberwiseClone();

        public abstract TSelf Build();
        
        protected void SetupAbilities()
        {
            if (!config || AbilityController == null) return;

            foreach (var passiveAbility in config.passiveAbilities)
            {
                AbilityController.AddPassiveAbility(passiveAbility);
            }
            foreach (var activeAbility in config.activeAbilities)
            {
                AbilityController.AddActiveAbility(activeAbility);
            }
        }
        
    }
    
    
}