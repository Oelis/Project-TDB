using Enums;

namespace Abilities.Effects.StatModif
{
    public class IceResistDown : DebuffStatModifierEffect
    {
        protected override StatType StatType => StatType.IceResist;
    }
}
