using Enums;

namespace Abilities.Effects.StatModif.Constitution
{
    public class ConstitutionDownDivide : StatModifierEffect
    {
        protected override StatType StatType => StatType.Constitution;

        protected override int Operation(Query query)
        {
            return query.Value / value;
        }
    }
}
