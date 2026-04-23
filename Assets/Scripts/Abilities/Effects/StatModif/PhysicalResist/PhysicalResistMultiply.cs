using Enums;

namespace Abilities.Effects.StatModif.PhysicalResist
{
    public class PhysicalResistMultiply : StatModifierEffect
    {
        protected override StatType StatType => StatType.PhysicalResist;

        protected override int Operation(Query query)
        {
            return query.Value * value;
        }
    }
}
