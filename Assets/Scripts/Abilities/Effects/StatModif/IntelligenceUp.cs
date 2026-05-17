using Enums;

namespace Abilities.Effects.StatModif
{
    public class IntelligenceUp : BuffStatModifierEffect
    {
        protected override StatType StatType => StatType.Intelligence;
    }
}
