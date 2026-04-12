using System;
using Interfaces;
using Sirenix.OdinInspector;
using Units;
using UnityEngine;

namespace Abilities.Effects

{
    [Serializable]
public abstract class EffectOverTime : IEffect
    {
        [MinValue(-1), ValidateInput(nameof(IsValidDuration), "Turn duration cannot be 0.")]
        public int _turnDuration = 1;
        private bool IsValidDuration(int value) => value != 0;
        public int MaxStackSize = 1;
        public bool CanBeCleanse = true;
        public abstract bool CanBeStacked { get; }

        protected IDamageable CurrentTarget;
        public event Action<EffectOverTime> OnCompleted;

        public virtual void Apply(UnitBrain source, UnitBrain target)
        {
            if (!target.ApplyEffect(this))
                
            {
                Debug.Log($"[{GetType().Name}] could not be applied to {target.GetType().Name}");
                return;
            }
            CurrentTarget = target;
            target.OnTurnStart += Tick;
            target.OnTurnEnd += CountDown;
            Debug.Log($"[{GetType().Name}] applied to {target.GetSource().name}");
        }

        public virtual void Cleanup()
        {
            OnCompleted?.Invoke(this);
            CurrentTarget.OnTurnStart -= Tick;
            CurrentTarget.OnTurnEnd -= CountDown;
            CurrentTarget = null;
            Debug.Log($"[{GetType().Name}] cleaned up");
        }
        
        private void CountDown()
        {
            
            switch (_turnDuration)
            {
                case -1:
                    Debug.Log($"[{GetType().Name}] has infinite duration.");
                    return;
                case > 0:
                    _turnDuration--;
                    Debug.Log($"[{GetType().Name}] duration reduced. Turns left: {_turnDuration}");
                    break;
            }
            if(_turnDuration == 0) Cleanup();
        }

        public virtual void Tick()
        {
            
        }

        
    }
}