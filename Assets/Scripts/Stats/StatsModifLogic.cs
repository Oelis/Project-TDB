using System;
using System.Collections.Generic;
using Abilities.Effects;

namespace Stats
{
    public class StatsModifLogic
    {
        private readonly LinkedList<StatModifierEffect> _modifiers = new();
        public event Action<Query> Queries;
        public void PerformQuery(Query query) => Queries?.Invoke(query);

        public void AddStatModifEffect(StatModifierEffect statModifier)
        {
            _modifiers.AddLast(statModifier);
            Queries += statModifier.Handle;
        }

        public void RemoveStatModifEffect(StatModifierEffect statModifier)
        {
            _modifiers.Remove(statModifier);
            Queries -= statModifier.Handle;
        }

        
    }
}