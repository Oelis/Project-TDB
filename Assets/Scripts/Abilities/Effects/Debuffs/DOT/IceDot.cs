using Enums;

namespace Abilities.Effects.Debuffs.DOT
{
    public class IceDot : DamageOverTimeEffect
    {
        
        public override bool CanBeStacked => true;
        public override StatType ResistanceStat => StatType.IceResist;
    }
}