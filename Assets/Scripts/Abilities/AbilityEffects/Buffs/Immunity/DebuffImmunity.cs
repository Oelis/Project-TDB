using Abilities.AbilityEffects.Debuffs;
using Attributes;
using Enums;
using Interfaces;

namespace Abilities.AbilityEffects.Buffs.Immunity
{
    [Immunity(typeof(IDebuff))]
    public class DebuffImmunity : ImmunityEffect, IBuff
    {
        
    }
}