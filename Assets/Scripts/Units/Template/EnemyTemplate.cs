using System.Collections.Generic;
using Interfaces;
using Sirenix.OdinInspector;
using Units.Logic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Units.Template
{
    [CreateAssetMenu(fileName = "EnemyTemplate", menuName = "Unit/Enemy")]
    public class EnemyTemplate : ScriptableObject
    {
        [SerializeReference,Required] public EnemyBrain enemyBrain; 
        [Required]  public EnemyConfig enemyConfig;
        [Required] public GameObject model;
    }
}