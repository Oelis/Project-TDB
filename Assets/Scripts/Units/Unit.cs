using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Abilities.AbilityEffects;
using Abilities.AbilityEffects.Buffs;
using Abilities.AbilityEffects.Buffs.Immunity;
using Abilities.AbilityEffects.Debuffs;
using Attributes;
using Enums;
using Interfaces;
using Stats;
using UnityEngine;

namespace Units
{
    public abstract class Unit : MonoBehaviour, IDamageable
    {
        public event Action OnTurnStart;
        public event Action OnTurnEnd;
        public float health;
    
        public readonly UnitConfig StatConfig; 
        protected readonly List<EffectOverTime> ongoingEffects = new();
        protected readonly List<ImmunityEffect> immunityeffects = new();
        // Separate static caches
        private static readonly ConcurrentDictionary<Type, Type[]> TypeImmunityCache = new();
        private static readonly ConcurrentDictionary<Type, DamageType[]> DamageTypeImmunityCache = new();

        public Stats.Stats Stats { get; private set; }

        protected virtual void Awake()
        {
            Stats = new Stats.Stats(new StatsMediator(), StatConfig);
        }

        public virtual void TakeDamage(float damage, DamageType damageType)
        {
            if (IsDamageImmuneTo(damageType)) return;
            health -= damage;
            if (health <= 0)
            {
                Die();
            }
        }

        public virtual void Die()
        {
            foreach (var effect in ongoingEffects)
            {
                effect.OnCompleted -= RemoveEffect;
                effect.Cleanup();
            }
            Destroy(gameObject);
        }
        
        private static readonly Dictionary<EffectType, Type> ImmunityTypeMap = new()
        {
            { EffectType.Buff, typeof(BuffImmunity) },
            { EffectType.Debuff, typeof(DebuffImmunity) }
        };
        
        public bool CanApplyEffect(EffectOverTime effect)
        {
            
            if (effect.EffectType == EffectType.Buff && ongoingEffects.OfType<BuffImmunity>().Any())
                return false;
            if (effect.EffectType == EffectType.Debuff && ongoingEffects.OfType<DebuffImmunity>().Any())
                return false;

            var effectType = effect.GetType();

            return !immunityeffects.Any(immunity =>
            {
                
                var immuneToTypes = GetTypeImmunityInfo(immunity.GetType());
                
                // Check type immunity
                if (immuneToTypes.Any(immuneType => immuneType.IsAssignableFrom(effectType)))
                    return true;
                
                return false;
            });
            
            return true;
        }
        
        
        private static Type[] GetTypeImmunityInfo(Type immunityType)
        {
            return TypeImmunityCache.GetOrAdd(immunityType, type =>
            {
                var attributes = type.GetCustomAttributes<ImmunityAttribute>();
        
                var allImmuneToTypes = new List<Type>();
        
                foreach (var attribute in attributes)
                {
                    allImmuneToTypes.AddRange(attribute.ImmuneToTypes);
                }
        
                return allImmuneToTypes.ToArray();
            });
        }


        private static DamageType[] GetDamageTypeImmunityInfo(Type immunityType)
        {
            return DamageTypeImmunityCache.GetOrAdd(immunityType, type =>
            {
                var attributes = type.GetCustomAttributes<ImmunityAttribute>();

                var allImmuneToDamageTypes = new List<DamageType>();

                foreach (var attribute in attributes)
                {
                    allImmuneToDamageTypes.AddRange(attribute.ImmuneToDamageTypes);
                }

                return allImmuneToDamageTypes.ToArray();
            });
        }

        public bool IsDamageImmuneTo(DamageType type)
        {
            return immunityeffects.Any(immunity =>
            {
                var immuneToDamageTypes = GetDamageTypeImmunityInfo(immunity.GetType());
                return immuneToDamageTypes.Contains(type);
            });
        }

        public virtual void ApplyEffect(EffectOverTime effect)
        {
            if(!CanApplyEffect(effect)) return;
            effect.OnCompleted += RemoveEffect;
            ongoingEffects.Add(effect);
            if (effect is ImmunityEffect immunity)
            {
                immunityeffects.Add(immunity);
            }
        }


        public virtual void RemoveEffect(EffectOverTime effect)
        {
            effect.OnCompleted -= RemoveEffect;
            ongoingEffects.Remove(effect);
            if (effect is ImmunityEffect immunity)
            {
                immunityeffects.Remove(immunity);
            }
        }
    
    }
}
