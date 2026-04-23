using Enums;

namespace Abilities.Effects.StatModif.PhysicalResist
{
    public class PhysicalResistDownSubtract : StatModifierEffect
    {
        protected override StatType StatType => StatType.PhysicalResist;

        public override int Operation(Query query)
        {
            return query.Value - value;
        }
    }
}
