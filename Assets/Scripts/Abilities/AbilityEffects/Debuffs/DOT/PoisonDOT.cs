using Enums;

namespace Abilities.AbilityEffects.Debuffs.DOT
{
    public class PoisonDOT : DamageOverTimeEffect
    {
        public override DamageType DamageType => DamageType.PoisonDamage;
    }
}