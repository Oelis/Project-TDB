using Abilities.Effects;
using Enums;

namespace Abilities.Effects.StatModif
{
    public class AttackUp : BuffStatModifierEffect
    {
        protected override StatType StatType => StatType.Attack;
    }
}
