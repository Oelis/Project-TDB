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
using UnityEngine;

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

        private bool _isFirstTurn = true;
        
        public Stats.Stats Stats {get; protected set; } 
        
        private float currentHealth;

        public virtual void StartTurn()
        {
            Debug.Log($"{source.name} turn started.");
            OnTurnStart?.Invoke();
            if (_isFirstTurn)
            {
                _isFirstTurn = false;
                AbilityController?.CastPassiveAbilities();
            }
        }
        
        public virtual void EndTurn()
        {
            Debug.Log($"[{GetType().Name}] ({source.name}) ended turn.");
            OnTurnEnd?.Invoke();
        }
        
        public virtual void TakeDamage(float damage, Type sourceEffectType)
        {
            if (immunityLogic.IsImmuneToEffectType(sourceEffectType)) return;
            // Check if evaded
            // Check if blocked
            // Calculate final damage output
            Debug.Log($"{source.name} took {damage} {sourceEffectType.Name} damage. HP: {currentHealth} -> {currentHealth - damage}");
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
        
        public bool ApplyEffect(EffectOverTime effect)
        {
            if(immunityLogic.IsImmuneToEffectType(effect.GetType())) return false;
            
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

            return true;
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
            Debug.Log($"[{GetType().Name}] ({source.name}) removed effect: {effect.GetType().Name}");
        }

        public AbilityController GetAbilityController()
        {
            return AbilityController;
        }

        public Unit GetSource()
        {
            return source;
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
                AbilityController.AddPassiveAbility(UnityEngine.Object.Instantiate(passiveAbility));
            }
            foreach (var activeAbility in config.activeAbilities)
            {
                AbilityController.AddActiveAbility(UnityEngine.Object.Instantiate(activeAbility));
            }
        }
        
    }
    
    
}