using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour, IDamageable
{
    public event Action OnTurnStart;
    public event Action OnTurnEnd;
    public float health;
    
    [SerializeField] UnitConfig _config; 
    protected readonly List<IEffect<IDamageable>> activeEffects = new();
    public Stats Stats { get; private set; }

    protected virtual void Awake()
    {
        Stats = new Stats(new StatsMediator(), _config);
    }
    
    public abstract void TakeDamage(float damage, DamageType damageType);
    public abstract void Die();
    public abstract void ApplyEffect(IEffect<IDamageable> effectOverTime);

    public abstract void RemoveEffect(IEffect<IDamageable> effectOverTime);
    
}
