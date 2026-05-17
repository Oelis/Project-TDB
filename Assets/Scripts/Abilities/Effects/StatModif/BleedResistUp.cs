using Enums;

namespace Abilities.Effects.StatModif
{
    public class BleedResistUp : BuffStatModifierEffect
    {
        protected override StatType StatType => StatType.BleedResist;
    }
}
