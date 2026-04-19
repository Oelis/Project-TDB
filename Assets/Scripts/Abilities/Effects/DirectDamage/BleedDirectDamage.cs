using Enums;

namespace Abilities.Effects.DirectDamage
{
    public class BleedDirectDamage : DirectDamageEffect
    {
        public override StatType ResistanceStat => StatType.BleedResist;
    }
}