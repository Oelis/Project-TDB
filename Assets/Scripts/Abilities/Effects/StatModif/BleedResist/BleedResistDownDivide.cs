using Enums;

namespace Abilities.Effects.StatModif.BleedResist
{
    public class BleedResistDownDivide : StatModifierEffect
    {
        protected override StatType StatType => StatType.BleedResist;

        protected override int Operation(Query query)
        {
            return query.Value / value;
        }
    }
}
