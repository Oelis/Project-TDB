using Enums;

namespace Abilities.Effects.StatModif
{
    public class StrengthUp : BuffStatModifierEffect
    {
        protected override StatType StatType => StatType.Strength;
    }
}
