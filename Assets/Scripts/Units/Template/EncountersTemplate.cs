using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Units.Template
{
    [CreateAssetMenu(fileName = "Encounters", menuName = "Unit/Encounters")]
    public class EncountersTemplate : ScriptableObject
    {
        [Required] public List<EnemyTemplate> enemies;
    }
}