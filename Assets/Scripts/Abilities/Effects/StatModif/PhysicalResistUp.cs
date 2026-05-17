using Enums;

namespace Abilities.Effects.StatModif
{
    public class PhysicalResistUp : BuffStatModifierEffect
    {
        protected override StatType StatType => StatType.PhysicalResist;
    }
}
