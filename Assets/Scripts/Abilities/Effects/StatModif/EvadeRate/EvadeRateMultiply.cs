using Enums;

namespace Abilities.Effects.StatModif.EvadeRate
{
    public class EvadeRateMultiply : StatModifierEffect
    {
        protected override StatType StatType => StatType.EvadeRate;

        public override int Operation(Query query)
        {
            return query.Value * value;
        }
    }
}
