using Enums;

namespace Abilities.Effects.StatModif
{
    public class IceResistUp : BuffStatModifierEffect
    {
        protected override StatType StatType => StatType.IceResist;
    }
}
