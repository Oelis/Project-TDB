using Enums;

namespace Abilities.Effects.StatModif.Intelligence
{
    public class IntelligenceDownDivide : StatModifierEffect
    {
        protected override StatType StatType => StatType.Intelligence;

        public override int Operation(Query query)
        {
            return query.Value / value;
        }
    }
}
