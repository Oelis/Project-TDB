using Stats;
using Units.Logic;
using UnityEngine;

namespace Units.Factory
{
    public class EnemyFactory : MonoBehaviour
    {
        public void CreateEnemy<T>(GameObject prefab, T enemy, EnemyConfig config) where T : EnemyBrain
        {
            var instance = Instantiate(prefab);
            var unit = instance.GetComponent<Unit<T>>();
            var brain = unit._unitBrain;
            brain.WithSource(unit).
                WithAbilityManager().
                WithConfig(config).
                WithStats(new Stats.Stats(new StatsMediator(), config)).
                Build();
        }
    }
}