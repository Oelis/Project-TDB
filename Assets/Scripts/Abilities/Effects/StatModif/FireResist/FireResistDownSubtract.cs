using Enums;

namespace Abilities.Effects.StatModif.FireResist
{
    public class FireResistDownSubtract : StatModifierEffect
    {
        protected override StatType StatType => StatType.FireResist;

        protected override int Operation(Query query)
        {
            return query.Value - value;
        }
    }
}
