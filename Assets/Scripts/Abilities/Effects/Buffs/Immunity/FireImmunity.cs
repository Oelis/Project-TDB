using Abilities.Effects.Debuffs.DOT;
using Attributes;
using Enums;
using Interfaces;

namespace Abilities.Effects.Buffs.Immunity
{
    [Immunity (typeof(FireDot))]
    [Immunity (DamageType.FireDamage)]
    
    public class FireImmunity : ImmunityEffect, IBuff
    {
    }
}