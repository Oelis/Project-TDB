using Abilities.AbilityEffects.Debuffs.DOT;
using Attributes;
using Enums;
using Interfaces;

namespace Abilities.AbilityEffects.Buffs.Immunity
{
    [Immunity (typeof(BleedDot))]
    [Immunity (DamageType.BleedDamage)]
    public class BleedImmunity : ImmunityEffect, IBuff
    {
    }
}