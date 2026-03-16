using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public float health = 100;
    
    readonly List<IEffectOverTime<IDamageable>> activeEffects = new();
    
    void Awake() => Registery<IDamageable>.TryAdd(this);

    public void ApplyEffect(IEffectOverTime<IDamageable> effectOverTime)
    {
        effectOverTime.OnCompleted += RemoveEffect;
        activeEffects.Add(effectOverTime);
        effectOverTime.Apply(this);
    }

    void RemoveEffect(IEffectOverTime<IDamageable> effectOverTime)
    {
        effectOverTime.OnCompleted -= RemoveEffect;
        activeEffects.Remove(effectOverTime);
    }

    void OnDestroy() => Registery<IDamageable>.Remove(this);
    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    void Die()
    {
        foreach (var effect in activeEffects)
        {
            effect.OnCompleted -= RemoveEffect;
            effect.Cancel();
        }
        Debug.Log("Enemy died.");
        Destroy(gameObject);
    }
}