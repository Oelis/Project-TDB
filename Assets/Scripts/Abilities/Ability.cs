using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;


namespace Abilities
{
    public abstract class Ability : SerializedScriptableObject
    {
        [Required] public string abilityName;

        [Required] public string description;

        [Required] public Texture icon;

        [OdinSerialize, HideReferenceObjectPicker, ListDrawerSettings(ListElementLabelName = nameof(EffectEntry.Label), CustomAddFunction = nameof(AddEffectEntry))]
        public List<EffectEntry> effectEntries = new();

        private EffectEntry AddEffectEntry() => new EffectEntry();

        private void OnEnable()
        {
            if (string.IsNullOrEmpty(abilityName)) abilityName = this.name;
        }
    }
}