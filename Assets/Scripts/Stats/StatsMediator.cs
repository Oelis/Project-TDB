using System;
using System.Collections.Generic;

public class StatsMediator
{
    readonly LinkedList<StatModifier> modifiers = new();
    public event EventHandler<Query> Queries;
    public void PerformQuery(object sender, Query query) => Queries.Invoke(sender, query);

    public void AddModifier(StatModifier modifier)
    {
        modifiers.AddLast(modifier);
        Queries += modifier.Handle;

        modifier.OnDispose += _ =>
        {
            modifiers.Remove(modifier);
            Queries -= modifier.Handle;
        };
    }
}