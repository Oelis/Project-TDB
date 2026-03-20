using Enums;

namespace Abilities.Effects.Debuffs.DOT
{
    public class FireDot: DamageOverTimeEffect

    {
        public override DamageType DamageType => DamageType.FireDamage;
        public override bool CanBeStacked => true;
    }
}