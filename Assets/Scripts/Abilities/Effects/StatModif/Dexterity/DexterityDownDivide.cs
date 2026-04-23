using Enums;

namespace Abilities.Effects.StatModif.Dexterity
{
    public class DexterityDownDivide : StatModifierEffect
    {
        protected override StatType StatType => StatType.Dexterity;

        public override int Operation(Query query)
        {
            return query.Value / value;
        }
    }
}
