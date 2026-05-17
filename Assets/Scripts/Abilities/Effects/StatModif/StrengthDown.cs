using Enums;

namespace Abilities.Effects.StatModif
{
    public class StrengthDown : DebuffStatModifierEffect
    {
        protected override StatType StatType => StatType.Strength;
    }
}
