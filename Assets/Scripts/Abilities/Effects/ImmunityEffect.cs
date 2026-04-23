using System;

namespace Abilities.Effects
{
    [Serializable]
    public abstract class ImmunityEffect : EffectOverTime
    {
        public override bool CanBeStacked => false;
        public override int MaxStackSize => 1;

    }
}