using Abilities.Effects;
using Enums;

namespace Abilities.Effects.StatModif
{
    public class AttackDown : DebuffStatModifierEffect
    {
        protected override StatType StatType => StatType.Attack;
    }
}
