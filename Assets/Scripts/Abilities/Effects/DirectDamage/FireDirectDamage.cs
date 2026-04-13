using Enums;

namespace Abilities.Effects.DirectDamage
{
    public class FireDirectDamage : DirectDamageEffect
    {
        public override StatType ResistanceStat => StatType.FireResist;
    }
}