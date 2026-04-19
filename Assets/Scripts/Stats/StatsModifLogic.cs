using System;
using System.Collections.Generic;

namespace Stats
{
    public class StatsModifLogic
    {
        private readonly LinkedList<StatModifier> _modifiers = new();
        public event Action<Query> Queries;
        public void PerformQuery(Query query) => Queries?.Invoke(query);

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