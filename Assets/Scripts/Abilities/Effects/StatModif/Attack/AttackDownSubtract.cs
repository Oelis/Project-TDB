using Enums;

namespace Abilities.Effects.StatModif.Attack
{
    public class AttackDownSubtract : StatModifierEffect
    {
        protected override StatType StatType => StatType.Attack;

        protected override int Operation(Query query)
        {
            return query.Value - value;
        }
    }
}
