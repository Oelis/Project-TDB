using System;
using System.Collections.Generic;
using System.Linq;
using Units;
using Units.Brain;

namespace Abilities.Targeting
{
    [Serializable]
    public class AllEnemiesTargeting : ITargetingStrategy
    {
        public IEnumerable<UnitBrain> Resolve() => Registry<EnemyBrain>.All;
    }
}
