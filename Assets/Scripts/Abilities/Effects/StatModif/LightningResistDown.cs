using Enums;

namespace Abilities.Effects.StatModif
{
    public class LightningResistDown : DebuffStatModifierEffect
    {
        protected override StatType StatType => StatType.LightningResist;
    }
}
