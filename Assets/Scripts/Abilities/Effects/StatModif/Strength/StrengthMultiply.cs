using Enums;

namespace Abilities.Effects.StatModif.Strength
{
    public class StrengthMultiply : StatModifierEffect
    {
        protected override StatType StatType => StatType.Strength;

        protected override int Operation(Query query)
        {
            return query.Value * value;
        }
    }
}
