using Enums;

namespace Abilities.Effects.Debuffs.DOT
{
    public class PoisonDot : DamageOverTimeEffect
    {
        
        public override bool CanBeStacked => true;
        public override StatType ResistanceStat => StatType.PoisonResist;
    }
}
