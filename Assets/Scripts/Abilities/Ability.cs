using System.Collections.Generic;
using Interfaces;
using UnityEngine;


namespace Abilities
{
    public abstract class Ability : ScriptableObject
    {
        public string abilityName;
    
        public string description;
    
        public Texture icon;
    
        [SerializeReference] public List<IEffect> effects = new();

        private void OnEnable()
        {
            if (string.IsNullOrEmpty(abilityName)) abilityName = this.name;
        }
    }


    [CreateAssetMenu(fileName = "ActiveAbility", menuName = "Spell/ActiveAbility")]
    public class ActiveAbility : Ability
    {
        public AnimationClip animationClip;
    }

    [CreateAssetMenu(fileName = "PassiveAbility", menuName = "Spell/PassiveAbility")]
    public class PassiveAbility : Ability
    {
    
    }
}