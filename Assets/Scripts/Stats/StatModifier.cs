using System;
using Enums;

namespace Stats
{
    public class BasicStatModifier : StatModifier
    {
        readonly StatType type;
        readonly Func<int, int> operation;

        public BasicStatModifier(StatType type, int turnDuration, Func<int, int> operation) : base(turnDuration)
        {
            this.type = type;
            this.operation = operation;
        }

        public override void Handle(object sender, Query query)
        {
            if (query.StatType == type)
            {
                query.Value = operation(query.Value);   
            }
        }
    }

    public abstract class StatModifier : IDisposable
    {
        public bool MarkedForRemoval { get; private set; }
    
        public event Action<StatModifier> OnDispose = delegate { };
    
        readonly int turnDuration;

        protected StatModifier(int turns)
        {
            if (turns < 0) return;
            else
            {
                turnDuration = turns;
            }
            // When Turns == 0 Dispose
        }
        public abstract void Handle(object sender, Query query);
        public void Dispose()
        {
            MarkedForRemoval = true;
            OnDispose.Invoke(this);
        }
    }
}