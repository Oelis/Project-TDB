using Enums;

namespace Abilities.Effects.StatModif.BleedResist
{
    public class BleedResistAdd : StatModifierEffect
    {
        protected override StatType StatType => StatType.BleedResist;

        public override int Operation(Query query)
        {
            return query.Value + value;
        }
    }
}
