using Abilities.Targeting;
using Interfaces;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Abilities
{
    public class EffectEntry
    {
        [OdinSerialize] public IEffect effect;
        [OdinSerialize, InlineProperty] public ITargetingStrategy targeting;

        public string Label => $"{effect?.GetType().Name ?? "Empty"} ({targeting?.GetType().Name.Replace("Targeting", "") ?? "None"})";
    }
}

