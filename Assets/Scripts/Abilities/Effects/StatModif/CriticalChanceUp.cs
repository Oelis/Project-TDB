using Enums;

namespace Abilities.Effects.StatModif
{
    public class CriticalChanceUp : BuffStatModifierEffect
    {
        protected override StatType StatType => StatType.CriticalChance;
    }
}
