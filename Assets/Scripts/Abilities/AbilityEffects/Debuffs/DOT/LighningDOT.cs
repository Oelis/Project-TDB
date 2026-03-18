using Enums;

namespace Abilities.AbilityEffects.Debuffs.DOT
{
    public class LighningDOT : DamageOverTimeEffect
    {
        public override DamageType DamageType => DamageType.LightningDamage;
    }
}