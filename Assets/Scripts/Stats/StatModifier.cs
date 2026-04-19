using System;
using Enums;

namespace Stats
{
    public class BasicStatModifier : StatModifier
    {
        private readonly StatType _type;
        private readonly Func<int, int> _operation;
        
        public BasicStatModifier(StatType type, int turnDuration, Func<int, int> operation) 
        {
            this._type = type;
            this._operation = operation;
        }

        public override void Handle(Query query)
        {
            if (query.StatType == _type)
            {
                query.Value = _operation(query.Value);
            }
        }
    }

    public abstract class StatModifier
    {
        public abstract void Handle(Query query);
    
        public event Action<StatModifier> OnDispose = delegate { };
        
        public void Dispose()
        {
            OnDispose.Invoke(this);
        }
    }
}