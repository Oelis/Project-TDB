using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class AbilityExecutor : MonoBehaviour
{
    [SerializeField] private ActiveAbility ability;
    [SerializeField] private List<PassiveAbility> passiveAbilities;
    [SerializeField] private GameObject target;
    
}
