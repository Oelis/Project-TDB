using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Units.Template
{
    [CreateAssetMenu(fileName = "PlayerSquad", menuName = "Unit/PlayerSquad")]
    public class PlayerSquad : ScriptableObject
    {
        private const int maxTileAmount = 10; 
        public readonly PlayerTemplate[] Squad = new PlayerTemplate[maxTileAmount];
        
        public void AddPlayer(PlayerTemplate player, int index)
        {
            if (!Squad[Mathf.Clamp(index - 1, 0, Squad.Length - 1)])
            {
                Squad[Mathf.Clamp(index - 1, 0, Squad.Length - 1)] = player;
                Debug.Log($"[PlayerSquad] {string.Join(", ", Array.ConvertAll(Squad, p => p ? p.name : "empty"))}");
                return;
            }
        }

        
        
        
    }
}