using System;
using Enums;

namespace Stats
{
    public class BasicStatModifier : StatModifier
    {
        private readonly StatType _type;
        private readonly Func<int, int> _operation;

        public BasicStatModifier(StatType type, int turnDuration, Func<int, int> operation) : base(turnDuration)
        {
            this._type = type;
            this._operation = operation;
        }

        public override void Handle(object sender, Query query)
        {
            if (query.StatType == _type)
            {
                query.Value = _operation(query.Value);   
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