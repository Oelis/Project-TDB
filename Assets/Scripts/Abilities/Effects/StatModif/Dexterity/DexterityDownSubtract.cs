using Enums;

namespace Abilities.Effects.StatModif.Dexterity
{
    public class DexterityDownSubtract : StatModifierEffect
    {
        protected override StatType StatType => StatType.Dexterity;

        protected override int Operation(Query query)
        {
            return query.Value - value;
        }
    }
}
