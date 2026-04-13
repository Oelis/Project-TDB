using Enums;

namespace Abilities.Effects.Debuffs.DOT
{
    public class FireDot: DamageOverTimeEffect

    {
        public override bool CanBeStacked => true;
        public override StatType ResistanceStat => StatType.FireResist;
    }
}