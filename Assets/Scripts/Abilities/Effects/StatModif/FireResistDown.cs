using Enums;

namespace Abilities.Effects.StatModif
{
    public class FireResistDown : DebuffStatModifierEffect
    {
        protected override StatType StatType => StatType.FireResist;
    }
}
