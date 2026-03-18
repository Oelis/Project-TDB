
namespace Abilities.AbilityEffects
{
    public abstract class ImmunityEffect : EffectOverTime
    {
        public override bool CanBeStacked => false;
    }
}