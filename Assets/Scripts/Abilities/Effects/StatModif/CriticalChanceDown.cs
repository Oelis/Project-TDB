using Enums;

namespace Abilities.Effects.StatModif
{
    public class CriticalChanceDown : DebuffStatModifierEffect
    {
        protected override StatType StatType => StatType.CriticalChance;
    }
}
