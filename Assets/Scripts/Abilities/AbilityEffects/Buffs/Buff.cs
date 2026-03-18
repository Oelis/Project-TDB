using Enums;

namespace Abilities.AbilityEffects.Buffs
{
    public abstract class Buff : EffectOverTime
    {
        public override EffectType EffectType => EffectType.Buff;
    }
}