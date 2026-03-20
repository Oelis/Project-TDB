using System;
using UnityEngine;

namespace Units.Template
{
    [CreateAssetMenu(fileName = "PlayerSquad", menuName = "Unit/PlayerSquad")]
    public class PlayerSquad : ScriptableObject
    {
        public readonly PlayerTemplate[] playerSquad = new PlayerTemplate[4];

        public bool IsFull() => Array.TrueForAll(playerSquad, p => p);

        public void AddPlayer(PlayerTemplate player)
        {
            if (IsFull()) return;

            for (int i = 0; i < playerSquad.Length; i++)
            {
                if (!playerSquad[i])
                {
                    playerSquad[i] = player;
                    Debug.Log($"[PlayerSquad] {string.Join(", ", Array.ConvertAll(playerSquad, p => p ? p.name : "empty"))}");
                    return;
                }
            }
            
            
        }

        public void ClearPlayerSquad()
        {
            Array.Clear(playerSquad, 0, playerSquad.Length);
        }
        
        
    }
}