using Attributes;
using Enums;

namespace Abilities.Effects.Debuffs
{
    [Immunity(EOTType.Buff)]
    public class BuffImmunity : ImmunityEffect
    {
        public override EOTType EOTType => EOTType.Debuff;
    }
}
