using Abilities.Effects.Debuffs.DOT;
using Attributes;
using Enums;
using Interfaces;

namespace Abilities.Effects.Buffs.Immunity
{
    [Immunity (typeof(LighningDot))]
    [Immunity (DamageType.LightningDamage)]
    public class LightningImmunity : ImmunityEffect, IBuff
    {
    }
}