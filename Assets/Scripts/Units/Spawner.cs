using System;
using Sirenix.OdinInspector;
using Units.Factory;
using Units.Template;
using UnityEngine;

namespace Units
{
    public class Spawner : MonoBehaviour
    {
        [LabelText("Enemy Grid")]
        public SpawnGrid enemyGrid = new SpawnGrid { origin = new Vector3(5f, 0f, 0f) };
        [LabelText("Player Grid")]
        public SpawnGrid playerGrid = new SpawnGrid { origin = new Vector3(-5f, 0f, 0f) };

        private UnitFactory _factory = new UnitFactory();
        
        public void SpawnEnemies(EncountersTemplate encountersTemplate)
        {
            for (int i = 0; i < encountersTemplate.slots.Length; i++)
            {
                var template = encountersTemplate.slots[i];
                if (!template) continue;
                _factory.CreateEnemy(template, enemyGrid.GetTilePosition(i), enemyGrid.GetTileRotation());
            }
        }

        public void SpawnPlayers(PlayerSquad playerSquad)
        {
            for (int i = 0; i < playerSquad.Squad.Length; i++)
            {
                var template = playerSquad.Squad[i];
                if (!template) continue;
                _factory.CreatePlayer(template, playerGrid.GetTilePosition(i), playerGrid.GetTileRotation());
            }
        }

        private void OnDrawGizmosSelected()
        {
            enemyGrid.DrawGizmos(Color.red);
            playerGrid.DrawGizmos(Color.blue);
        }
    }
}