using Attributes;
using Interfaces;

namespace Abilities.Effects.Buffs.Immunity
{
    [Immunity(typeof(IDebuff))]
    public class DebuffImmunity : ImmunityEffect, IBuff
    {
        
    }
}