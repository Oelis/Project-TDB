using System;
using Enums;

namespace Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ImmunityAttribute : Attribute
    {
        public DamageType[] ImmuneToDamageTypes { get; }
        public EOTType[] ImmuneToEOTTypes { get; }

        public ImmunityAttribute(params DamageType[] immuneToDamageTypes)
        {
            ImmuneToDamageTypes = immuneToDamageTypes ?? Array.Empty<DamageType>();
            ImmuneToEOTTypes = Array.Empty<EOTType>();
        }

        public ImmunityAttribute(params EOTType[] immuneToEOTTypes)
        {
            ImmuneToDamageTypes = Array.Empty<DamageType>();
            ImmuneToEOTTypes = immuneToEOTTypes ?? Array.Empty<EOTType>();
        }
    }
}
