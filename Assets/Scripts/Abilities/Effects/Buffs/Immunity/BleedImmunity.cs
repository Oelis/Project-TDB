using Abilities.Effects.Debuffs.DOT;
using Attributes;
using Enums;
using Interfaces;

namespace Abilities.Effects.Buffs.Immunity
{
    [Immunity (typeof(BleedDot))]
    [Immunity (DamageType.BleedDamage)]
    public class BleedImmunity : ImmunityEffect, IBuff
    {
    }
}