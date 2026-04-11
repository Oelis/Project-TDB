using System;
using System.Collections.Generic;
using System.Linq;
using Units;

namespace Abilities.Targeting
{
    [Serializable]
    public class SelfTargeting : ICasterTargetingStrategy
    {
        
        public IEnumerable<UnitBrain> Resolve(UnitBrain source) { yield return source; }
        public IEnumerable<UnitBrain> Resolve()=> Enumerable.Empty<UnitBrain>(); 
        
    }
}
