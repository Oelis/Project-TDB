using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private PlayerInput playerInput;
    
    
    void Awake()
    {
        Registery<IDamageable>.TryAdd(this);
    }
    
    IDamageable GetClosest(IEnumerable<IDamageable> candidates)
    {
        IDamageable closest = null;
        float minDistance = float.MaxValue;

        foreach (var candidate in candidates)
        {
            if (candidate == null) continue;
            if(candidate is not Component component) continue; 
            
            float distance = Vector3.Distance(transform.position, component.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = candidate;
            }
        }
        
        return closest;
    }


    public void TakeDamage(float damage)
    {
        throw new System.NotImplementedException();
    }

    void OnDestroy()
    {
        Registery<IDamageable>.Remove(this);
    }
}