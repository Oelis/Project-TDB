using System.Collections.Generic;
using Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;


namespace Abilities
{
    public abstract class Ability : ScriptableObject
    {
        [Required] public string abilityName;
    
        [Required] public string description;
    
        [Required] public Texture icon;
    
        [SerializeReference,Required] public List<IEffect> effects = new();

        private void OnEnable()
        {
            if (string.IsNullOrEmpty(abilityName)) abilityName = this.name;
        }
    }
}