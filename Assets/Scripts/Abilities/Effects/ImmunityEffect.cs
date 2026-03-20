
namespace Abilities.Effects
{
    public abstract class ImmunityEffect : EffectOverTime
    {
        public override bool CanBeStacked => false;
    }
}