using Enums;

namespace Abilities.Effects.DirectDamage
{
    public class IceDirectDamage : DirectDamageEffect
    {
        public override StatType ResistanceStat => StatType.IceResist;
    }
}