using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit, IDamageable
{
    public override void TakeDamage(float damage, DamageType damageType)
    {
        throw new NotImplementedException();
    }

    public override void Die()
    {
        throw new NotImplementedException();
    }

    public override void ApplyEffect(IEffect<IDamageable> effectOverTime)
    {
        throw new NotImplementedException();
    }

    public override void RemoveEffect(IEffect<IDamageable> effectOverTime)
    {
        throw new NotImplementedException();
    }
}