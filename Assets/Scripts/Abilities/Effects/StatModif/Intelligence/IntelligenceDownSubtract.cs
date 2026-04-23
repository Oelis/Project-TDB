using Enums;

namespace Abilities.Effects.StatModif.Intelligence
{
    public class IntelligenceDownSubtract : StatModifierEffect
    {
        protected override StatType StatType => StatType.Intelligence;

        protected override int Operation(Query query)
        {
            return query.Value - value;
        }
    }
}
