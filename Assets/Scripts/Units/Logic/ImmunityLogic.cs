using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Abilities;
using Abilities.Effects;
using Attributes;
using Enums;

namespace Units.Logic
{
    public class ImmunityLogic
    {
        // Separate static caches
        
        private List<ImmunityEffect> _immunityeffects = new();
        
        private static readonly ConcurrentDictionary<Type, Type[]> TypeImmunityCache = new();
        private static readonly ConcurrentDictionary<Type, DamageType[]> DamageTypeImmunityCache = new();
        
        public static Type[] GetTypeImmunityInfo(Type immunityType)
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
        
        public static DamageType[] GetDamageTypeImmunityInfo(Type immunityType)
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
            return _immunityeffects.Any(immunity =>
            {
                var immuneToDamageTypes = GetDamageTypeImmunityInfo(immunity.GetType());
                return immuneToDamageTypes.Contains(type);
            });
        }
        
        public bool BlockEffect(EffectOverTime effect)
        {
            var effectType = effect.GetType();

            // Return TRUE if NO immunity blocks it (can apply)
            // Return FALSE if ANY immunity blocks it (cannot apply)
            return _immunityeffects.Any(immunity =>
            {
                var immuneToTypes = GetTypeImmunityInfo(immunity.GetType());
        
                // Check if this immunity blocks the effect type
                return immuneToTypes.Any(immuneType => immuneType.IsAssignableFrom(effectType));
            });
        }

        public List<ImmunityEffect> GetImmunityEffects()
        {
            return _immunityeffects;
        }
        
        public void AddImmunityEffect(ImmunityEffect effect)
        {
            _immunityeffects.Add(effect);
        }
        
        public void RemoveImmunityEffect(ImmunityEffect effect)
        {
            _immunityeffects.Remove(effect);
        }
    }
}