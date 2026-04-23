using Enums;

namespace Abilities.Effects.StatModif.BlockRate
{
    public class BlockRateDownSubtract : StatModifierEffect
    {
        protected override StatType StatType => StatType.BlockRate;

        protected override int Operation(Query query)
        {
            return query.Value - value;
        }
    }
}
