using Enums;

namespace Abilities.AbilityEffects.Debuffs
{
    public abstract class Debuff : EffectOverTime
    {
        public override EffectType EffectType => EffectType.Debuff;
    }
}