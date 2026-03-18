using Abilities.AbilityEffects.Debuffs.DOT;
using Attributes;
using Enums;

namespace Abilities.AbilityEffects.Buffs.Immunity
{
    [Immunity (typeof(LighningDOT))]
    [Immunity (DamageType.LightningDamage)]
    public class LightningImmunity : Buff
    {
        
    }
}