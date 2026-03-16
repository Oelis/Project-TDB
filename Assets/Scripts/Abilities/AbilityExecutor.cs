using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class AbilityExecutor : MonoBehaviour
{
    [SerializeField] private AbilityData ability;
    [SerializeField] private List<AbilityData> passiveAbilities;
    [SerializeField] private GameObject target;

    public void Execute(GameObject pTarget)
    {
        foreach (var effect in  ability.effects)
        {
            effect.Execute(gameObject,pTarget);
        }
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
           Execute(target); 
        }
    }
}
