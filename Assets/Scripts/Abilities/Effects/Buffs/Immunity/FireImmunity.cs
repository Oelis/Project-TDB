using Abilities.Effects.Debuffs.DOT;
using Abilities.Effects.DirectDamage;
using Attributes;
using Interfaces;

namespace Abilities.Effects.Buffs.Immunity
{
    [Immunity(typeof(FireDot))]
    [Immunity(typeof(FireDirectDamage))]
    public class FireImmunity : ImmunityEffect, IBuff
    {
    }
}