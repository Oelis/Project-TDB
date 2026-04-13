using Abilities.Effects.Debuffs.DOT;
using Abilities.Effects.DirectDamage;
using Attributes;
using Interfaces;

namespace Abilities.Effects.Buffs.Immunity
{
    [Immunity(typeof(IceDot))]
    [Immunity(typeof(IceDirectDamage))]
    public class IceImmunity : ImmunityEffect, IBuff
    {
    }
}