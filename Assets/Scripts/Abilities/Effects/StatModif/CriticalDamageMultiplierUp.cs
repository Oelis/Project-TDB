using Enums;

namespace Abilities.Effects.StatModif
{
    public class CriticalDamageMultiplierUp : BuffStatModifierEffect
    {
        protected override StatType StatType => StatType.CriticalDamageMultiplier;
    }
}
