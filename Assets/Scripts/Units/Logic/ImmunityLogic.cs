using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Abilities.Effects;
using Attributes;
using Enums;

namespace Units.Logic
{
    public class ImmunityLogic
    {
        private List<ImmunityEffect> _immunityeffects = new();

        private static readonly ConcurrentDictionary<Type, (DamageType[] DamageTypes, EOTType[] EOTTypes)> ImmunityCache = new();

        public static (DamageType[] DamageTypes, EOTType[] EOTTypes) GetImmunityInfo(Type immunityType)
        {
            return ImmunityCache.GetOrAdd(immunityType, type =>
            {
                var attributes = type.GetCustomAttributes<ImmunityAttribute>();
                var damageTypes = new List<DamageType>();
                var eotTypes = new List<EOTType>();
                foreach (var attr in attributes)
                {
                    damageTypes.AddRange(attr.ImmuneToDamageTypes);
                    eotTypes.AddRange(attr.ImmuneToEOTTypes);
                }
                return (damageTypes.ToArray(), eotTypes.ToArray());
            });
        }

        public bool IsImmuneToDamageType(DamageType damageType)
        {
            return _immunityeffects.Any(immunity =>
            {
                var (damageTypes, _) = GetImmunityInfo(immunity.GetType());
                return damageTypes.Contains(damageType);
            });
        }

        public bool IsImmuneToEOTType(EOTType eotType)
        {
            return _immunityeffects.Any(immunity =>
            {
                var (_, eotTypes) = GetImmunityInfo(immunity.GetType());
                return eotTypes.Contains(eotType);
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
