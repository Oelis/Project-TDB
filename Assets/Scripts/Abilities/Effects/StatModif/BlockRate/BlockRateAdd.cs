using Enums;

namespace Abilities.Effects.StatModif.BlockRate
{
    public class BlockRateAdd : StatModifierEffect
    {
        protected override StatType StatType => StatType.BlockRate;

        public override int Operation(Query query)
        {
            return query.Value + value;
        }
    }
}
