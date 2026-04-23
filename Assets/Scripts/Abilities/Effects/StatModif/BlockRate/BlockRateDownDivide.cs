using Enums;

namespace Abilities.Effects.StatModif.BlockRate
{
    public class BlockRateDownDivide : StatModifierEffect
    {
        protected override StatType StatType => StatType.BlockRate;

        public override int Operation(Query query)
        {
            return query.Value / value;
        }
    }
}
