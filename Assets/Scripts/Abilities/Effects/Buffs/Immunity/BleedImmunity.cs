using Abilities.Effects.Debuffs.DOT;
using Abilities.Effects.DirectDamage;
using Attributes;
using Interfaces;

namespace Abilities.Effects.Buffs.Immunity
{
    [Immunity(typeof(BleedDot))]
    [Immunity(typeof(BleedDirectDamage))]
    public class BleedImmunity : ImmunityEffect, IBuff
    {
    }
}