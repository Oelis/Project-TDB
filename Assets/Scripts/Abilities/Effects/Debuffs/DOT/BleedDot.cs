using System;
using Enums;

namespace Abilities.Effects.Debuffs.DOT
{
    [Serializable]
    public class BleedDot: DamageOverTimeEffect

    {
        
        public override bool CanBeStacked => true;

        public override StatType ResistanceStat => StatType.BleedResist;
    }
}