using Enums;

namespace Abilities.Effects.DirectDamage
{
    public class PhysicalDirectDamage : DirectDamageEffect
    {
        public override StatType ResistanceStat => StatType.PhysicalResist;
    }
}