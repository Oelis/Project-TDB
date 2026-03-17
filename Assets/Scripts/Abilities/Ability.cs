using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public abstract class Ability : ScriptableObject
{
    public string name;
    
    public string description;
    
    public Texture icon;
    
    [SerializeReference] public List<IEffect<IDamageable>> effects = new();

    void OnEnable()
    {
        if (string.IsNullOrEmpty(name)) name = ((Object)this).name;
    }
}


[CreateAssetMenu(fileName = "ActiveAbility", menuName = "Spell/ActiveAbility")]
public class ActiveAbility : Ability
{
    public AnimationClip animationClip;
}

[CreateAssetMenu(fileName = "PassiveAbility", menuName = "Spell/PassiveAbility")]
public class PassiveAbility : Ability
{
    
}