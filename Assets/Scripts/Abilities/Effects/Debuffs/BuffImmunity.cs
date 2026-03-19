using Abilities.AbilityEffects.Buffs;
using Abilities.AbilityEffects.Debuffs.DOT;
using Attributes;
using Enums;
using Interfaces;
using Unity.VisualScripting;


namespace Abilities.AbilityEffects.Debuffs
{
    [Immunity(typeof(IBuff))]
    public class BuffImmunity : ImmunityEffect, IDebuff
    {
        
    }

    
}