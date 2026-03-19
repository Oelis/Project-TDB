using Abilities.AbilityEffects.Debuffs.DOT;
using Attributes;
using Enums;
using Interfaces;

namespace Abilities.AbilityEffects.Buffs.Immunity
{
    [Immunity (typeof(LighningDot))]
    [Immunity (DamageType.LightningDamage)]
    public class LightningImmunity : ImmunityEffect, IBuff
    {
    }
}