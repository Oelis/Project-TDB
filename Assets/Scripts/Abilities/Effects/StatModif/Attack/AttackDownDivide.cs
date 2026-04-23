using Enums;

namespace Abilities.Effects.StatModif.Attack
{
    public class AttackDownDivide : StatModifierEffect
    {
        protected override StatType StatType => StatType.Attack;

        public override int Operation(Query query)
        {
            return query.Value / value;
        }
    }
}