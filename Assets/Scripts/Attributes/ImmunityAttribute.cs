using System;

namespace Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ImmunityAttribute : Attribute
    {
        public Type[] ImmuneToTypes { get; }

        public ImmunityAttribute(params Type[] immuneToTypes)
        {
            ImmuneToTypes = immuneToTypes ?? Array.Empty<Type>();
        }
    }
}