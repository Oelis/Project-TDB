using Enums;

namespace Abilities.Effects.StatModif
{
    public class BlockRateUp : BuffStatModifierEffect
    {
        protected override StatType StatType => StatType.BlockRate;
    }
}
