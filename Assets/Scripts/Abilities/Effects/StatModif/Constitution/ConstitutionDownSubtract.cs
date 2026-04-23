using Enums;

namespace Abilities.Effects.StatModif.Constitution
{
    public class ConstitutionDownSubtract : StatModifierEffect
    {
        protected override StatType StatType => StatType.Constitution;

        public override int Operation(Query query)
        {
            return query.Value - value;
        }
    }
}
