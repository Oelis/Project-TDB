using Enums;

namespace Abilities.Effects.StatModif.Defense
{
    public class DefenseMultiply : StatModifierEffect
    {
        protected override StatType StatType => StatType.Defense;

        protected override int Operation(Query query)
        {
            return query.Value * value;
        }
    }
}
