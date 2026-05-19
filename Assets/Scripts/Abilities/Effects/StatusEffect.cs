using Interfaces;
using Units;

namespace Abilities.Effects
{
    public abstract class StatusEffect : IEffect
    {
        public void Apply(UnitBrain source, UnitBrain target)
        {
            
        }

        public void Cleanup()
        {
            
        }
    }
}