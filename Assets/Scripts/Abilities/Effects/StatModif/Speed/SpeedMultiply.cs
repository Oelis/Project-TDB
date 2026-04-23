using Enums;

namespace Abilities.Effects.StatModif.Speed
{
    public class SpeedMultiply : StatModifierEffect
    {
        protected override StatType StatType => StatType.Speed;

        public override int Operation(Query query)
        {
            return query.Value * value;
        }
    }
}
