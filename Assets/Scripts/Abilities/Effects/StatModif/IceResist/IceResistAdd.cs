using Enums;

namespace Abilities.Effects.StatModif.IceResist
{
    public class IceResistAdd : StatModifierEffect
    {
        protected override StatType StatType => StatType.IceResist;

        protected override int Operation(Query query)
        {
            return query.Value + value;
        }
    }
}
