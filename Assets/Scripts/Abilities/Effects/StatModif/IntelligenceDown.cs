using Enums;

namespace Abilities.Effects.StatModif
{
    public class IntelligenceDown : DebuffStatModifierEffect
    {
        protected override StatType StatType => StatType.Intelligence;
    }
}
