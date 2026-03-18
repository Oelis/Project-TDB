using Enums;

namespace Abilities.AbilityEffects.Debuffs.DOT
{
    public class BleedDOT: DamageOverTimeEffect

    {
        public override DamageType DamageType => DamageType.BleedDamage;
    }
}