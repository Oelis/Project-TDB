using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityData : ScriptableObject
{
    public string name;
    
    public string description;
    
    public Texture icon;
    
    [SerializeReference] public List<AbilityEffect> effects;

    void OnEnable()
    {
        if (string.IsNullOrEmpty(name)) name = ((Object)this).name;
        if (effects == null) effects = new List<AbilityEffect>();
    }
}
[CreateAssetMenu(fileName = "AbilityData", menuName = "Spell/ActiveAbility")]
class ActiveAbility : AbilityData
{
    public AnimationClip animationClip;
}

[CreateAssetMenu(fileName = "AbilityData", menuName = "Spell/PassiveAbility")]
class PassiveAbility : AbilityData
{
    
}