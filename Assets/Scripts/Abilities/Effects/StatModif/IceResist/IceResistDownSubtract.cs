using Enums;

namespace Abilities.Effects.StatModif.IceResist
{
    public class IceResistDownSubtract : StatModifierEffect
    {
        protected override StatType StatType => StatType.IceResist;

        public override int Operation(Query query)
        {
            return query.Value - value;
        }
    }
}
