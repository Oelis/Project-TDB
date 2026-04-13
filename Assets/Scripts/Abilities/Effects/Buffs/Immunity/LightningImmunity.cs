using Abilities.Effects.Debuffs.DOT;
using Abilities.Effects.DirectDamage;
using Attributes;
using Interfaces;

namespace Abilities.Effects.Buffs.Immunity
{
    [Immunity(typeof(LighningDot))]
    [Immunity(typeof(LightningDirectDamage))]
    public class LightningImmunity : ImmunityEffect, IBuff
    {
    }
}