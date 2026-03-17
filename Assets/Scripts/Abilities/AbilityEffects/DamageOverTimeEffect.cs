using System;

[Serializable]
public abstract class DamageOverTimeEffect : IEffect<IDamageable>
{
    public int damagePerTurn;
    public int turnDuration = 1;
    public event Action<IEffect<IDamageable>> OnCompleted;
    
    protected IDamageable currentTarget;
    
    public void Apply(Unit source,IDamageable target)
    {
        currentTarget = target;
        currentTarget.ApplyEffect(this);
        currentTarget.OnTurnStart+=Tick;
        currentTarget.OnTurnEnd+=Countdown;
    }

    public abstract void Tick();
    
    public void Cancel()
    {
        Cleanup();
    }

    public void Countdown()
    {
        switch (turnDuration)
        {
            case -1:
                return;
            case > 0:
                turnDuration--;
                break;
        }
        if(turnDuration == 0)
            Cleanup();
    }

    public void Cleanup()
    {
        currentTarget.OnTurnStart-=Tick;
        currentTarget.OnTurnEnd-=Countdown;
        currentTarget = null;
    }
}