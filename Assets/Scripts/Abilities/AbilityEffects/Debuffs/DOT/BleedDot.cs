using Enums;

namespace Abilities.AbilityEffects.Debuffs.DOT
{
    public class BleedDot: DamageOverTimeEffect

    {
        public override DamageType DamageType => DamageType.BleedDamage;
        public override bool CanBeStacked => true;
    }
}