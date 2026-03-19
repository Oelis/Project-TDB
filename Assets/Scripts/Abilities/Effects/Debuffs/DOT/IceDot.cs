using Enums;

namespace Abilities.AbilityEffects.Debuffs.DOT
{
    public class IceDot : DamageOverTimeEffect
    {
        public override DamageType DamageType => DamageType.IceDamage;
        public override bool CanBeStacked => true;
    }
}