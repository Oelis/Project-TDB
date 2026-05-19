using System;
using Enums;

namespace Abilities.Effects
{
    [Serializable]
    public class SafeguardBuff : EffectOverTime
    {
        public override EOTType EOTType => EOTType.Buff;
        public override int MaxStackSize => 1;
    }
}
