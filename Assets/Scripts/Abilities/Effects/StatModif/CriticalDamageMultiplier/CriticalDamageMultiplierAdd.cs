using Enums;

namespace Abilities.Effects.StatModif.CriticalDamageMultiplier
{
    public class CriticalDamageMultiplierAdd : StatModifierEffect
    {
        protected override StatType StatType => StatType.CriticalDamageMultiplier;

        public override int Operation(Query query)
        {
            return query.Value + value;
        }
    }
}
