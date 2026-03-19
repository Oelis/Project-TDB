using Abilities.AbilityEffects.Debuffs.DOT;
using Attributes;
using Enums;
using Interfaces;

namespace Abilities.AbilityEffects.Buffs.Immunity
{
    [Immunity (typeof(IceDot))]
    [Immunity (DamageType.IceDamage)]
    public class IceImmunity : ImmunityEffect, IBuff
    {
    }
}