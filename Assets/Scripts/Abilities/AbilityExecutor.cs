using System.Collections.Generic;
using UnityEngine;

namespace Abilities
{
    public class AbilityExecutor : MonoBehaviour
    {
        [SerializeField] private ActiveAbility ability;
        [SerializeField] private List<PassiveAbility> passiveAbilities;
        [SerializeField] private GameObject target;
    
    }
}
