using Attributes;
using Enums;
using Interfaces;

namespace Abilities.Effects.Buffs.Immunity
{
    [Immunity(DamageType.BleedDamage)]
    public class BleedImmunity : ImmunityEffect
    {
        public override EOTType EOTType => EOTType.Buff;
    }
}
