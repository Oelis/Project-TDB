using Enums;

namespace Abilities.Effects.StatModif.BlockRate
{
    public class BlockRateMultiply : StatModifierEffect
    {
        protected override StatType StatType => StatType.BlockRate;

        protected override int Operation(Query query)
        {
            return query.Value * value;
        }
    }
}
