using System.Collections.Generic;
using Abilities.Effects;

namespace Units.Logic
{
    public class HotLogic
    {
        private List<HealOverTime> _hotEffects = new();

        public List<HealOverTime> GetHealOverTime() => _hotEffects;

        public void AddHealOverTime(HealOverTime effect) => _hotEffects.Add(effect);

        public void RemoveHealOverTime(HealOverTime effect) => _hotEffects.Remove(effect);
    }
}
