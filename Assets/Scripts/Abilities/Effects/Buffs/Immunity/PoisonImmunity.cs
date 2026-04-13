using Abilities.Effects.Debuffs.DOT;
using Abilities.Effects.DirectDamage;
using Attributes;
using Interfaces;

namespace Abilities.Effects.Buffs.Immunity
{
    [Immunity(typeof(PoisonDot))]
    [Immunity(typeof(PoisonDirectDamage))]
    public class PoisonImmunity : ImmunityEffect, IBuff
    {
    }
}