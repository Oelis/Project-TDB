using Enums;

namespace Abilities.Effects.StatModif.CriticalChance
{
    public class CriticalChanceAdd : StatModifierEffect
    {
        protected override StatType StatType => StatType.CriticalChance;

        public override int Operation(Query query)
        {
            return query.Value + value;
        }
    }
}
