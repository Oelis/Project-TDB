using System;
using Enums;
using Interfaces;
using Units;

namespace Abilities.AbilityEffects

{
    public abstract class EffectOverTime : IEffect
    {
        private int _turnDuration = 1;
        public int MaxStackSize = 1;
        public bool CanBeCleanse = true;
        public abstract bool CanBeStacked { get; }

        protected IDamageable CurrentTarget;
        public event Action<EffectOverTime> OnCompleted;

        public virtual void Apply(UnitBrain source, UnitBrain target)
        {
            CurrentTarget = target;
            CurrentTarget.ApplyEffect(this);
            CurrentTarget.OnTurnStart += Tick;
            CurrentTarget.OnTurnEnd += CountDown;
        }

        public virtual void Cleanup()
        {
            OnCompleted?.Invoke(this);
            CurrentTarget.OnTurnStart -= Tick;
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

        public virtual void Tick()
        {
            
        }

        
    }
}