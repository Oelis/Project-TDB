using Attributes;
using Enums;

namespace Abilities.Effects.Buffs.Immunity
{
    [Immunity(EOTType.Debuff)]
    public class DebuffImmunity : ImmunityEffect
    {
        public override EOTType EOTType => EOTType.Buff;
    }
}
