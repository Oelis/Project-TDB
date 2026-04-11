using System;
using System.Collections.Generic;
using System.Linq;
using Units;
using Units.Brain;
using Random = UnityEngine.Random;

namespace Abilities.Targeting
{
    [Serializable]
    public class RandomEnemyTargeting : ITargetingStrategy
    {
        public IEnumerable<UnitBrain> Resolve()
        {
            var enemies = Registry<EnemyBrain>.All.ToList();
            if (enemies.Count == 0) yield break;
            yield return enemies[Random.Range(0, enemies.Count)];
        }
    }
}
