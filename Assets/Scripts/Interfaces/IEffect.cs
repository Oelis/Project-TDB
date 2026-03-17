using System;

public interface IEffect<TTarget>
{
    void Apply(Unit source, TTarget target);
    void Cancel();
    event Action<IEffect<TTarget>> OnCompleted;
}