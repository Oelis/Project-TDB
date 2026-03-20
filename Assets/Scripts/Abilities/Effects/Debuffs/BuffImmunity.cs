using Attributes;
using Interfaces;

namespace Abilities.Effects.Debuffs
{
    [Immunity(typeof(IBuff))]
    public class BuffImmunity : ImmunityEffect, IDebuff
    {
        
    }

    
}