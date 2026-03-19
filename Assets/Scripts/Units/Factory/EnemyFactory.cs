using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Stats;
using Units.Logic;
using Units.Template;
using Unity.VisualScripting;
using UnityEngine;

namespace Units.Factory
{
    public class EnemyFactory : MonoBehaviour
    {
        // Need to be pass by the level manager later instead of serialized
        [SerializeField] EncountersTemplate encounters;
        public void CreateEnemy(EnemyTemplate template)
        {
            var instance = Instantiate(template.model);
            var unit = instance.GetComponent<Unit>();
            var brain = template.enemyBrain.Clone();
            brain.WithSource(unit).
                WithAbilityManager().
                WithConfig(template.enemyConfig).
                WithStats(new Stats.Stats(new StatsMediator(),template.enemyConfig)).
                Build();
            unit.SetBrain(brain);
        }

        private void Start()
        {
            foreach (var enemy in encounters.enemies)
            {
                CreateEnemy(enemy);
            }
        }
    }
}