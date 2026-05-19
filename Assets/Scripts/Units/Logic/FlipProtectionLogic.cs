using System.Collections.Generic;
using Abilities.Effects;

namespace Units.Logic
{
    public class FlipProtectionLogic
    {
        private readonly List<SafeguardBuff> _safeguardBuffs = new();
        private readonly List<TraumaDebuff> _traumaDebuffs = new();

        public bool HasSafeguard() => _safeguardBuffs.Count > 0;
        public bool HasTrauma()    => _traumaDebuffs.Count > 0;

        public void Add(SafeguardBuff effect)    => _safeguardBuffs.Add(effect);
        public void Remove(SafeguardBuff effect) => _safeguardBuffs.Remove(effect);

        public void Add(TraumaDebuff effect)    => _traumaDebuffs.Add(effect);
        public void Remove(TraumaDebuff effect) => _traumaDebuffs.Remove(effect);

        public IEnumerable<EffectOverTime> GetAllEffects()
        {
            foreach (var e in _safeguardBuffs) yield return e;
            foreach (var e in _traumaDebuffs)  yield return e;
        }
    }
}
