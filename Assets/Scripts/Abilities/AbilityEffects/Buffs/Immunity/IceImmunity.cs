using Abilities.AbilityEffects.Debuffs.DOT;
using Attributes;
using Enums;

namespace Abilities.AbilityEffects.Buffs.Immunity
{
    [Immunity (typeof(IceDOT))]
    [Immunity (DamageType.IceDamage)]
    public class IceImmunity : Buff
    {
        
    }
}