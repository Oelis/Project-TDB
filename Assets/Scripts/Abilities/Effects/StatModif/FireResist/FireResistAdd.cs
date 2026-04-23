using Enums;

namespace Abilities.Effects.StatModif.FireResist
{
    public class FireResistAdd : StatModifierEffect
    {
        protected override StatType StatType => StatType.FireResist;

        public override int Operation(Query query)
        {
            return query.Value + value;
        }
    }
}
