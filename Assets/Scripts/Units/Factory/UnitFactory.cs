using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Stats;
using Units.Brain;
using Units.Logic;
using Units.Template;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Units.Factory
{
    public class UnitFactory 
    {
        public GameObject CreateEnemy(EnemyTemplate template, Vector3 position, Quaternion rotation)
        {
            var instance = Object.Instantiate(template.model, position, rotation);
            var unit = instance.GetComponent<Unit>();
            var brainType = template.enemyBrain.GetType();
            var brain = (EnemyBrain)Activator.CreateInstance(brainType);
            brain.WithSource(unit).
                WithAbilityManager().
                WithConfig(template.enemyConfig).
                WithStats().
                Build();
            unit.SetBrain(brain);
            Registry<EnemyBrain>.TryAdd(brain);
            return instance;
            
        }

        public GameObject CreatePlayer(PlayerTemplate template, Vector3 position, Quaternion rotation)
        {
            var instance = Object.Instantiate(template.model, position, rotation);
            var unit = instance.GetComponent<Unit>();
            var brain = (PlayerBrain)Activator.CreateInstance(template.playerBrain.GetType()); 
            brain.WithSource(unit).
                WithAbilityManager().
                WithConfig(template.playerConfig).
                WithStats().
                Build();
            unit.SetBrain(brain);
            Registry<PlayerBrain>.TryAdd(brain);
            return instance;
        }

        
    }
}