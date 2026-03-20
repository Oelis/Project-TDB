using System;
using Units.Factory;
using Units.Template;
using UnityEngine;

namespace Units
{
    public class Spawner : MonoBehaviour
    {
        private UnitFactory _factory = new UnitFactory();
        
        private EncountersTemplate _encountersTemplate;
        
        private PlayerSquad _playerSquad;
        
        public void SetPlayerSquad(PlayerSquad playerSquad)
        {
            _playerSquad = playerSquad;
        }
        
        public void SetEncounters(EncountersTemplate encountersTemplate)
        {
            _encountersTemplate = encountersTemplate;
        }

        public void SpawnEnemies()
        {
            foreach (var enemyTemplate in _encountersTemplate.enemies)
            {
                if (!enemyTemplate) continue;    
                _factory.CreateEnemy(enemyTemplate);
            }
        }

        public void SpawnPlayers()
        {
            foreach (var playerTemplate in _playerSquad.playerSquad)
            {
                if (!playerTemplate) continue;    
                _factory.CreatePlayer(playerTemplate);
            }
        }
    }

    
}