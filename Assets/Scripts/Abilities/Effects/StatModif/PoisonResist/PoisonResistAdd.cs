using Enums;

namespace Abilities.Effects.StatModif.PoisonResist
{
    public class PoisonResistAdd : StatModifierEffect
    {
        protected override StatType StatType => StatType.PoisonResist;

        protected override int Operation(Query query)
        {
            return query.Value + value;
        }
    }
}
