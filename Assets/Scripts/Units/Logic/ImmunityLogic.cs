using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Abilities.Effects;
using Attributes;

namespace Units.Logic
{
    public class ImmunityLogic
    {
        private List<ImmunityEffect> _immunityeffects = new();

        private static readonly ConcurrentDictionary<Type, Type[]> TypeImmunityCache = new();

        public static Type[] GetTypeImmunityInfo(Type immunityType)
        {
            return TypeImmunityCache.GetOrAdd(immunityType, type =>
            {
                var attributes = type.GetCustomAttributes<ImmunityAttribute>();
                var allImmuneToTypes = new List<Type>();
                foreach (var attribute in attributes)
                    allImmuneToTypes.AddRange(attribute.ImmuneToTypes);
                return allImmuneToTypes.ToArray();
            });
        }

        public bool IsImmuneToEffectType(Type effectType)
        {
            return _immunityeffects.Any(immunity =>
            {
                var immuneToTypes = GetTypeImmunityInfo(immunity.GetType());
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