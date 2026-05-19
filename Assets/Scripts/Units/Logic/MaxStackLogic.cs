using System.Collections.Generic;
using Abilities.Effects;

namespace Units.Logic
{
    public class MaxStackLogic
    {
        private readonly Dictionary<object, int> _currentStacks = new();

        public bool CanAddStack(EffectOverTime effect)
        {
            _currentStacks.TryGetValue(effect.StackKey, out int current);
            return current < effect.MaxStackSize;
        }

        public void AddStack(EffectOverTime effect)
        {
            _currentStacks.TryGetValue(effect.StackKey, out int current);
            _currentStacks[effect.StackKey] = current + 1;
        }

        public void RemoveStack(EffectOverTime effect)
        {
            var key = effect.StackKey;
            if (!_currentStacks.TryGetValue(key, out int current)) return;

            if (current <= 1)
                _currentStacks.Remove(key);
            else
                _currentStacks[key] = current - 1;
        }
    }
}
