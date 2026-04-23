using Enums;

namespace Abilities.Effects.StatModif.LightningResist
{
    public class LightningResistAdd : StatModifierEffect
    {
        protected override StatType StatType => StatType.LightningResist;

        protected override int Operation(Query query)
        {
            return query.Value + value;
        }
    }
}
