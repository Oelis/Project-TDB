using System;
using System.Collections.Generic;
using System.Linq;
using Units;
using Units.Brain;

namespace Abilities.Targeting
{
    [Serializable]
    public class AllAlliesTargeting : ITargetingStrategy
    {
        public IEnumerable<UnitBrain> Resolve() => Registry<PlayerBrain>.All;
    }
}
