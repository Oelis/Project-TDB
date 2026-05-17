using Enums;

namespace Abilities.Effects.StatModif
{
    public class PoisonResistDown : DebuffStatModifierEffect
    {
        protected override StatType StatType => StatType.PoisonResist;
    }
}
