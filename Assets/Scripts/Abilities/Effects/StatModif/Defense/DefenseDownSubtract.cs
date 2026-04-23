using Enums;

namespace Abilities.Effects.StatModif.Defense
{
    public class DefenseDownSubtract : StatModifierEffect
    {
        protected override StatType StatType => StatType.Defense;

        public override int Operation(Query query)
        {
            return query.Value - value;
        }
    }
}
