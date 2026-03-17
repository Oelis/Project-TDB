using System;

[Serializable]
public class DamageEffect : IEffect<IDamageable>
{
    public int damageAmount = 10;
    public DamageType damageType;
    public event Action<IEffect<IDamageable>> OnCompleted;
    public void Apply(IDamageable target)
    {
        target.TakeDamage(damageAmount,damageType);
        OnCompleted?.Invoke(this);
    }

    public void Cancel()
    {
        OnCompleted?.Invoke(this);
    }
}