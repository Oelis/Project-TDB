using Attributes;
using Enums;
using Interfaces;

namespace Abilities.Effects.Buffs.Immunity
{
    [Immunity(DamageType.LightningDamage)]
    public class LightningImmunity : ImmunityEffect
    {
        public override EOTType EOTType => EOTType.Buff;
    }
}
