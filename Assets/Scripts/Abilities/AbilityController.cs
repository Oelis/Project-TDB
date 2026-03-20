using System.Collections.Generic;
using Interfaces;
using Units;
using UnityEngine;

namespace Abilities
{
    public class AbilityController 
    {
        private List<ActiveAbility> _activeAbilities = new List<ActiveAbility>();   
        private List<PassiveAbility> _passiveAbilities = new List<PassiveAbility>();
        
        private UnitBrain source;
        
        public AbilityController(UnitBrain source)
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
            
            Debug.Log($"Passive Ability Added : {ability.name} on {source.GetType().Name}");
        }
        
        public void AddActiveAbility(ActiveAbility ability)
        {
            _activeAbilities.Add(ability);
            Debug.Log($"Active Ability Added : {ability.name} on {source.GetType().Name}");
        }

        public ActiveAbility GetAbility(int index)
        {
            return _activeAbilities[index];
        }
        
    }
}
