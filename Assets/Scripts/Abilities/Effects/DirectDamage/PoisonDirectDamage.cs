using Enums;

namespace Abilities.Effects.DirectDamage
{
    public class PoisonDirectDamage : DirectDamageEffect
    {
        public override StatType ResistanceStat => StatType.PoisonResist;
    }
}