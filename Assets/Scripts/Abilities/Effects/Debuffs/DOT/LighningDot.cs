using Enums;

namespace Abilities.AbilityEffects.Debuffs.DOT
{
    public class LighningDot : DamageOverTimeEffect
    {
        public override DamageType DamageType => DamageType.LightningDamage;
        public override bool CanBeStacked => true;
    }
}