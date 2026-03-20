using Abilities.Effects.Debuffs.DOT;
using Attributes;
using Enums;
using Interfaces;

namespace Abilities.Effects.Buffs.Immunity
{
    [Immunity (typeof(PoisonDot))]
    [Immunity (DamageType.PoisonDamage)]
    public class PoisonImmunity : ImmunityEffect, IBuff
    {
    }
}