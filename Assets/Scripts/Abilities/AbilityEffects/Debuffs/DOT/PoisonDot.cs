using Enums;

namespace Abilities.AbilityEffects.Debuffs.DOT
{
    public class PoisonDot : DamageOverTimeEffect
    {
        public override DamageType DamageType => DamageType.PoisonDamage;
        public override bool CanBeStacked => true;
    }
}
