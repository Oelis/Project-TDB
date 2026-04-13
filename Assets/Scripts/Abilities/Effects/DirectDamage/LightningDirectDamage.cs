using Enums;

namespace Abilities.Effects.DirectDamage
{
    public class LightningDirectDamage : DirectDamageEffect
    {
        public override StatType ResistanceStat => StatType.LightningResist;
    }
}