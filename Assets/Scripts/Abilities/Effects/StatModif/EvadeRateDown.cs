using Enums;

namespace Abilities.Effects.StatModif
{
    public class EvadeRateDown : DebuffStatModifierEffect
    {
        protected override StatType StatType => StatType.EvadeRate;
    }
}
