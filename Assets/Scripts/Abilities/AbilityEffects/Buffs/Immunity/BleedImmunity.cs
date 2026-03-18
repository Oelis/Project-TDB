using Abilities.AbilityEffects.Debuffs.DOT;
using Attributes;
using Enums;

namespace Abilities.AbilityEffects.Buffs.Immunity
{
    [Immunity (typeof(BleedDOT))]
    [Immunity (DamageType.BleedDamage)]
    public class BleedImmunity : Buff
    {
        
    }
}