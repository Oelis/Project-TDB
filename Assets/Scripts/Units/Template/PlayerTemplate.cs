using System.ComponentModel.DataAnnotations;
using Units.Brain;
using Units.Configs;
using Units.Logic;
using UnityEngine;

namespace Units.Template
{
    [CreateAssetMenu(fileName = "PlayerTemplate", menuName = "Unit/Template/Player")]
    public class PlayerTemplate : ScriptableObject
    {
        public PlayerBrain playerBrain { get;private set; } = new PlayerBrain(); 
        [Required] public PlayerConfig playerConfig;
        [Required] public GameObject model;
    }
}