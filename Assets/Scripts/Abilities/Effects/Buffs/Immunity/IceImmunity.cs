using Attributes;
using Enums;
using Interfaces;

namespace Abilities.Effects.Buffs.Immunity
{
    [Immunity(DamageType.IceDamage)]
    public class IceImmunity : ImmunityEffect
    {
        public override EOTType EOTType => EOTType.Buff;
    }
}
