using Enums;

namespace Abilities.Effects.StatModif
{
    public class PhysicalResistDown : DebuffStatModifierEffect
    {
        protected override StatType StatType => StatType.PhysicalResist;
    }
}
