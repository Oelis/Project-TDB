using System;
using Enums;

namespace Abilities.Effects.Debuffs.DOT
{
    [Serializable]
    public class BleedDot: DamageOverTimeEffect

    {
        public override DamageType DamageType => DamageType.BleedDamage;
        public override bool CanBeStacked => true;
    }
}