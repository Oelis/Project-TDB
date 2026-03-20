using Abilities.Effects.Debuffs.DOT;
using Attributes;
using Enums;
using Interfaces;

namespace Abilities.Effects.Buffs.Immunity
{
    [Immunity (typeof(IceDot))]
    [Immunity (DamageType.IceDamage)]
    public class IceImmunity : ImmunityEffect, IBuff
    {
    }
}