using Enums;

namespace Abilities.Effects.StatModif
{
    public class DefenseUp : BuffStatModifierEffect
    {
        protected override StatType StatType => StatType.Defense;
    }
}
