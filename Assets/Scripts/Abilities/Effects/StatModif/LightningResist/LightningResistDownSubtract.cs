using Enums;

namespace Abilities.Effects.StatModif.LightningResist
{
    public class LightningResistDownSubtract : StatModifierEffect
    {
        protected override StatType StatType => StatType.LightningResist;

        public override int Operation(Query query)
        {
            return query.Value - value;
        }
    }
}
