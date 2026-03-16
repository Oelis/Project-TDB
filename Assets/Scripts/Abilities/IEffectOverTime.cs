using System;

public interface IEffectOverTime<TTarget>
{
    void Apply(TTarget target);
    void Cancel();
    
    event Action<IEffectOverTime<TTarget>> OnCompleted;
}