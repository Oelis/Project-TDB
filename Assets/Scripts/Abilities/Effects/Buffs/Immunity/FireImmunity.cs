using Attributes;
using Enums;
using Interfaces;

namespace Abilities.Effects.Buffs.Immunity
{
    [Immunity(DamageType.FireDamage)]
    public class FireImmunity : ImmunityEffect
    {
        public override EOTType EOTType => EOTType.Buff;
    }
}
