using Enums;

namespace Abilities.Effects.StatModif
{
    public class BleedResistDown : DebuffStatModifierEffect
    {
        protected override StatType StatType => StatType.BleedResist;
    }
}
