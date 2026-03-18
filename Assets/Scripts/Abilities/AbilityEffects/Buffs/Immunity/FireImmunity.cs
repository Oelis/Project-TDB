using Abilities.AbilityEffects.Debuffs.DOT;
using Attributes;
using Enums;

namespace Abilities.AbilityEffects.Buffs.Immunity
{
    [Immunity (typeof(FireDOT))]
    [Immunity (DamageType.FireDamage)]
    
    public class FireImmunity : Buff

    {

    }
}