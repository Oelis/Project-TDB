using System.Collections.Generic;
using Units;

namespace Abilities.Targeting
{
    public interface ITargetingStrategy
    {
        IEnumerable<UnitBrain> Resolve();
    }

    public interface ICasterTargetingStrategy :  ITargetingStrategy
    {
        IEnumerable<UnitBrain> Resolve(UnitBrain source);
    }
}
