using UnityEngine;
public class Player : Unit, IDamageable
{
    public override void TakeDamage(float damage,DamageType damageType)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
    public override void Die()
    {
        foreach (var effect in activeEffects)
        {
            effect.OnCompleted -= RemoveEffect;
            effect.Cancel();
        }
        Debug.Log("Enemy died.");
        Destroy(gameObject);
    }

    public override void ApplyEffect(IEffect<IDamageable> effectOverTime)
    {
        effectOverTime.OnCompleted += RemoveEffect;
        activeEffects.Add(effectOverTime);
    }

    public override void RemoveEffect(IEffect<IDamageable> effectOverTime)
    {
        effectOverTime.OnCompleted -= RemoveEffect;
        activeEffects.Remove(effectOverTime);
    }
}