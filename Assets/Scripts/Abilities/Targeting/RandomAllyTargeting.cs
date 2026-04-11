using System;
using System.Collections.Generic;
using System.Linq;
using Units;
using Units.Brain;
using Random = UnityEngine.Random;

namespace Abilities.Targeting
{
    [Serializable]
    public class RandomAllyTargeting : ITargetingStrategy
    {
        public IEnumerable<UnitBrain> Resolve()
        {
            var allies = Registry<PlayerBrain>.All.ToList();
            if (allies.Count == 0) yield break;
            yield return allies[Random.Range(0, allies.Count)];
        }
    }
}
