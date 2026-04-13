using Enums;

namespace Abilities.Effects.Debuffs.DOT
{
    public class LighningDot : DamageOverTimeEffect
    {
        
        
        public override bool CanBeStacked => true;
        public override StatType ResistanceStat => StatType.LightningResist;
    }
}