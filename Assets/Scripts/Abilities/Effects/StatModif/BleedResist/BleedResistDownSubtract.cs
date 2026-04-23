using Enums;

namespace Abilities.Effects.StatModif.BleedResist
{
    public class BleedResistDownSubtract : StatModifierEffect
    {
        protected override StatType StatType => StatType.BleedResist;

        protected override int Operation(Query query)
        {
            return query.Value - value;
        }
    }
}
