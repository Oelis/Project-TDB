using Enums;

namespace Abilities.Effects.StatModif.Speed
{
    public class SpeedAdd : StatModifierEffect
    {
        protected override StatType StatType => StatType.Speed;

        protected override int Operation(Query query)
        {
            return query.Value + value;
        }
    }
}
