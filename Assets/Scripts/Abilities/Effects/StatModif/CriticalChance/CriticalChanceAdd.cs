using Enums;

namespace Abilities.Effects.StatModif.CriticalChance
{
    public class CriticalChanceAdd : StatModifierEffect
    {
        protected override StatType StatType => StatType.CriticalChance;

        protected override int Operation(Query query)
        {
            return query.Value + value;
        }
    }
}
