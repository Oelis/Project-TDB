using System;
using System.Collections.Generic;

namespace Stats
{
    public class StatsMediator
    {
        private readonly LinkedList<StatModifier> _modifiers = new();
        public event EventHandler<Query> Queries;
        public void PerformQuery(object sender, Query query) => Queries?.Invoke(sender, query);

        public void AddModifier(StatModifier modifier)
        {
            _modifiers.AddLast(modifier);
            Queries += modifier.Handle;

            modifier.OnDispose += _ =>
            {
                _modifiers.Remove(modifier);
                Queries -= modifier.Handle;
            };
        }
    }
}