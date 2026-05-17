using Enums;

namespace Abilities.Effects.StatModif
{
    public class CriticalDamageMultiplierDown : DebuffStatModifierEffect
    {
        protected override StatType StatType => StatType.CriticalDamageMultiplier;
    }
}
