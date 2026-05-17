using Enums;

namespace Abilities.Effects.StatModif
{
    public class PoisonResistUp : BuffStatModifierEffect
    {
        protected override StatType StatType => StatType.PoisonResist;
    }
}
