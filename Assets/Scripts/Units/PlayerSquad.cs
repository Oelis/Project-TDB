using System;
using UnityEngine;

namespace Units.Template
{
    [CreateAssetMenu(fileName = "PlayerSquad", menuName = "Unit/PlayerSquad")]
    public class PlayerSquad : ScriptableObject
    {
        public readonly PlayerTemplate[] Squad = new PlayerTemplate[4];

        public bool IsFull() => Array.TrueForAll(Squad, p => p);

        public void AddPlayer(PlayerTemplate player)
        {
            if (IsFull()) return;

            for (int i = 0; i < Squad.Length; i++)
            {
                if (!Squad[i])
                {
                    Squad[i] = player;
                    Debug.Log($"[PlayerSquad] {string.Join(", ", Array.ConvertAll(Squad, p => p ? p.name : "empty"))}");
                    return;
                }
            }
            
            
        }

        public void ClearPlayerSquad()
        {
            Array.Clear(Squad, 0, Squad.Length);
        }
        
        
    }
}