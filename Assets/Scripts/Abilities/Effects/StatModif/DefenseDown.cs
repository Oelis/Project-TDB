using Enums;

namespace Abilities.Effects.StatModif
{
    public class DefenseDown : DebuffStatModifierEffect
    {
        protected override StatType StatType => StatType.Defense;
    }
}
