using Enums;

namespace Abilities.Effects.StatModif
{
    public class SpeedDown : DebuffStatModifierEffect
    {
        protected override StatType StatType => StatType.Speed;
    }
}
