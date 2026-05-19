using System;
using Enums;

namespace Abilities.Effects
{
    [Serializable]
    public class TraumaDebuff : EffectOverTime
    {
        public override EOTType EOTType => EOTType.Debuff;
        public override int MaxStackSize => 1;
    }
}
