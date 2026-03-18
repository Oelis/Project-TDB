using Enums;

namespace Abilities.AbilityEffects.Debuffs.DOT
{
    public class IceDOT : DamageOverTimeEffect
    {
        public override DamageType DamageType => DamageType.IceDamage;
    }
}