using Attributes;
using Enums;
using Interfaces;

namespace Abilities.Effects.Buffs.Immunity
{
    [Immunity(DamageType.PoisonDamage)]
    public class PoisonImmunity : ImmunityEffect
    {
        public override EOTType EOTType => EOTType.Buff;
    }
}
