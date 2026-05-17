using System;
using System.Collections.Generic;
using Abilities.Effects;

namespace Units.Logic
{
    public class MaxStackLogic
    {
        private readonly Dictionary<Type, int> _currentStacks = new();

        public bool CanAddStack(EffectOverTime effect)
        {
            if (!effect.CanBeStacked) return false;
            _currentStacks.TryGetValue(effect.GetType(), out int current);
            return current < effect.MaxStackSize;
        }

        public void AddStack(EffectOverTime effect)
        {
            _currentStacks.TryGetValue(effect.GetType(), out int current);
            _currentStacks[effect.GetType()] = current + 1;
        }

        public void RemoveStack(EffectOverTime effect)
        {
            var type = effect.GetType();
            if (!_currentStacks.TryGetValue(type, out int current)) return;

            if (current <= 1)
                _currentStacks.Remove(type);
            else
                _currentStacks[type] = current - 1;
        }
    }
}
