using Abilities.AbilityEffects.Debuffs.DOT;
using Attributes;
using Enums;
using Interfaces;

namespace Abilities.AbilityEffects.Buffs.Immunity
{
    [Immunity (typeof(PoisonDot))]
    [Immunity (DamageType.PoisonDamage)]
    public class PoisonImmunity : ImmunityEffect, IBuff
    {
    }
}