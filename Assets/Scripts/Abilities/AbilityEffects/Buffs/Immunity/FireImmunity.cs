using Abilities.AbilityEffects.Debuffs.DOT;
using Attributes;
using Enums;
using Interfaces;

namespace Abilities.AbilityEffects.Buffs.Immunity
{
    [Immunity (typeof(FireDot))]
    [Immunity (DamageType.FireDamage)]
    
    public class FireImmunity : ImmunityEffect, IBuff
    {
    }
}