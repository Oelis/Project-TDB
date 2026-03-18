using Enums;

namespace Abilities.AbilityEffects.Debuffs.DOT
{
    public class FireDOT: DamageOverTimeEffect

    {
        public override DamageType DamageType => DamageType.FireDamage;
    }
}