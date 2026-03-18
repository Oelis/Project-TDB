using System;
using Enums;

namespace Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ImmunityAttribute : Attribute
    {
        public Type[] ImmuneToTypes { get; }
        public DamageType[] ImmuneToDamageTypes { get; }
    
        // Constructor for types only
        public ImmunityAttribute(params Type[] immuneToTypes)
        {
            ImmuneToTypes = immuneToTypes ?? Array.Empty<Type>();
            ImmuneToDamageTypes = Array.Empty<DamageType>();
        }
    
        // Constructor for damage types only
        public ImmunityAttribute(params DamageType[] immuneToDamageTypes)
        {
            ImmuneToDamageTypes = immuneToDamageTypes ?? Array.Empty<DamageType>();
            ImmuneToTypes = Array.Empty<Type>();
        }
    
        // Constructor for both
        public ImmunityAttribute(Type[] immuneToTypes, DamageType[] immuneToDamageTypes)
        {
            ImmuneToTypes = immuneToTypes ?? Array.Empty<Type>();
            ImmuneToDamageTypes = immuneToDamageTypes ?? Array.Empty<DamageType>();
        }
    }
}