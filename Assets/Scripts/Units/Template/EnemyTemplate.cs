using Sirenix.OdinInspector;
using Units.Brain;
using Units.Configs;
using Units.Logic;
using UnityEngine;

namespace Units.Template
{
    [CreateAssetMenu(fileName = "EnemyTemplate", menuName = "Unit/Template/Enemy")]
    public class EnemyTemplate : ScriptableObject
    {
        [SerializeReference, Required] public EnemyBrain enemyBrain;
        [Required] public EnemyConfig enemyConfig;
        [Required] public GameObject model;
    }
}