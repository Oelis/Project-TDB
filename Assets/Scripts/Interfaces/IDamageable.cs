using System;

public interface IDamageable
{
    void TakeDamage(float damage, DamageType type);

    void Die();

    void ApplyEffect(IEffect<IDamageable> effect);
    
    void RemoveEffect(IEffect<IDamageable> effect);

    public event Action OnTurnStart;
    public event Action OnTurnEnd;

}