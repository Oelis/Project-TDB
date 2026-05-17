using Enums;

namespace Abilities.Effects.StatModif
{
    public class BlockRateDown : DebuffStatModifierEffect
    {
        protected override StatType StatType => StatType.BlockRate;
    }
}
