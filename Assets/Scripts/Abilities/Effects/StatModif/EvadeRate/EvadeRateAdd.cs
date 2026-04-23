using Enums;

namespace Abilities.Effects.StatModif.EvadeRate
{
    public class EvadeRateAdd : StatModifierEffect
    {
        protected override StatType StatType => StatType.EvadeRate;

        protected override int Operation(Query query)
        {
            return query.Value + value;
        }
    }
}
