using System;
using System.Collections.Generic;
using Abilities;
using Abilities.Effects;
using Enums;
using Interfaces;
using Policies;
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

        protected readonly StatsModifLogic statModifLogic = new StatsModifLogic();

        private readonly MaxStackLogic maxStackLogic = new MaxStackLogic();
        
        private readonly StatusLogic statusLogic = new StatusLogic();
        
        protected AbilityController AbilityController;

        protected Unit source;

        private bool _isFirstTurn = true;

        private const int BLOCK_AMOUNT = 50;
        
        public Stats.Stats Stats {get; protected set; } 
        
        private int currentHealth;

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
        
        public virtual void TakeDamage(int damage, DamageType sourceEffectDamageType, bool canBeEvaded, bool canBeBlocked)
        {
            if (immunityLogic.IsImmuneToDamageType(sourceEffectDamageType)) return;
            
            int finalDamage = damage;
            
            if (canBeEvaded)
            {
                if (LuckPolicy.Create().Roll(Stats.EvadeRate)) return;
            }
            
            finalDamage = Mathf.RoundToInt(finalDamage * (1f - Stats.GetStat(Helpers.ResistDic[sourceEffectDamageType]) / 100f));
            
            if (canBeBlocked)
            {
                if (LuckPolicy.Create().Roll(Stats.BlockRate))
                {
                    finalDamage = Mathf.RoundToInt(finalDamage * (1f - BLOCK_AMOUNT / 100f));
                } 
            }
            
            Debug.Log($"{source.name} took {finalDamage} {sourceEffectDamageType} damage. HP: {currentHealth} -> {currentHealth - finalDamage}");
            currentHealth -= finalDamage;
        }

        public void Heal(int amount)
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
            if (immunityLogic.IsImmuneToEOTType(effect.EOTType)) return false;
            if (!maxStackLogic.CanAddStack(effect)) return false;

            maxStackLogic.AddStack(effect);
            
            effect.OnCompleted += RemoveEffect;
            
            switch (effect)
            {
                case ImmunityEffect immunity:
                    CleanupBlockedEffects(immunity);
                    immunityLogic.AddImmunityEffect(immunity);
                    break;
                case DamageOverTimeEffect damageOverTime:
                    dotLogic.AddDamageOvertime(damageOverTime);
                    break;
                case StatModifierEffect statModifier:
                    statModifLogic.AddStatModifEffect(statModifier);
                    break;
            }

            return true;
        }
        
        public void RemoveEffect(EffectOverTime effect)
        {
            effect.OnCompleted -= RemoveEffect;
            maxStackLogic.RemoveStack(effect);
            switch (effect)
            {
                case ImmunityEffect immunity:
                    immunityLogic.RemoveImmunityEffect(immunity);
                    break;
                case DamageOverTimeEffect damageOverTime:
                    dotLogic.RemoveDamageOvertime(damageOverTime);
                    break;
                case StatModifierEffect statModifier:
                    statModifLogic.RemoveStatModifEffect(statModifier);
                    break;
            }
            Debug.Log($"[{GetType().Name}] ({source.name}) removed effect: {effect.GetType().Name}");
        }

        private void CleanupBlockedEffects(ImmunityEffect immunity)
        {
            var (blockedDamageTypes, blockedEOTTypes) = ImmunityLogic.GetImmunityInfo(immunity.GetType());

            if (blockedEOTTypes.Length > 0)
            {
                CleanupBlocked(dotLogic.GetDamageOvertime(), blockedEOTTypes);
                CleanupBlocked(statModifLogic.GetModifiers(), blockedEOTTypes);
                CleanupBlocked(immunityLogic.GetImmunityEffects(), blockedEOTTypes);
            }

            if (blockedDamageTypes.Length > 0)
                CleanupBlocked(dotLogic.GetDamageOvertime(), blockedDamageTypes);
        }

        private void CleanupBlocked(IEnumerable<EffectOverTime> effectsOverTimes, EOTType[] blockedEOTTypes)
        {
            var allEffects = new List<EffectOverTime>(effectsOverTimes);
            foreach (var effect in allEffects)
            {
                if (System.Array.IndexOf(blockedEOTTypes, effect.EOTType) >= 0)
                    effect.Cleanup();
            }
        }

        private void CleanupBlocked(IEnumerable<EffectOverTime> effectsOverTimes, DamageType[] blockedDamageTypes)
        {
            var allEffects = new List<EffectOverTime>(effectsOverTimes);
            foreach (var effect in allEffects)
            {
                if (effect is DamageOverTimeEffect dot && System.Array.IndexOf(blockedDamageTypes, dot.DamageType) >= 0)
                    effect.Cleanup();
            }
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
        public TSelf WithStats() { this.Stats = new Stats.Stats(statModifLogic, config); return (TSelf)this; }
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