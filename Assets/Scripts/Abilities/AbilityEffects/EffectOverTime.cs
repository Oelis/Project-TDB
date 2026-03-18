using System;
using Enums;
using Interfaces;
using Units;

namespace Abilities.AbilityEffects

{
    public abstract class EffectOverTime : IEffect<IDamageable>
    {
        private int _turnDuration = 1;
        public int MaxStackSize = 1;
        public bool CanBeStacked = true;
        public bool CanBeCleanse = true;
        public abstract EffectType EffectType { get; }

        protected IDamageable CurrentTarget;
        public event Action<EffectOverTime> OnCompleted;

        public virtual void Apply(Unit source, IDamageable target)
        {
            CurrentTarget = target;
            if (!CurrentTarget.CanApplyEffect(this)) return;
            CurrentTarget.ApplyEffect(this);
            CurrentTarget.OnTurnEnd += CountDown;
        }

        public virtual void Cleanup()
        {
            OnCompleted?.Invoke(this);
            CurrentTarget.OnTurnEnd -= CountDown;
            CurrentTarget = null;
        }
        
        private void CountDown()
        {
            switch (_turnDuration)
            {
                case -1 :
                    return;
                case > 0 :
                    _turnDuration--;
                    break;
            }
            if(_turnDuration == 0) Cleanup();
        }

        
    }
}