using Enums;

namespace Abilities.Effects.StatModif
{
    public class EvadeRateUp : BuffStatModifierEffect
    {
        protected override StatType StatType => StatType.EvadeRate;
    }
}
