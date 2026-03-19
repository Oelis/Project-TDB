using System.Collections.Generic;
using Abilities.AbilityEffects;
using Interfaces;
using Units;
using UnityEngine;

namespace Abilities
{
    public class AbilityManager 
    {
        private List<ActiveAbility> _activeAbilities = new List<ActiveAbility>();   
        private List<PassiveAbility> _passiveAbilities = new List<PassiveAbility>();
        
        private UnitBrain source;
        
        public AbilityManager(UnitBrain source)
        {
            this.source = source;
        }
        public void CastAbility(ActiveAbility abilityToCast, UnitBrain target)
        {
            foreach (IEffect abilityEffect in abilityToCast.effects)
            {
                abilityEffect.Apply(source,target);
            }
        }

        public void AddPassiveAbility(PassiveAbility ability)
        {
            _passiveAbilities.Add(ability);
            
            Debug.Log("Passive Ability Added : " + ability.name + "");
        }
        
        public void AddActiveAbility(ActiveAbility ability)
        {
            _activeAbilities.Add(ability);
            Debug.Log("Active Ability Added : " + ability.name + "");
        }
        
    }
}
