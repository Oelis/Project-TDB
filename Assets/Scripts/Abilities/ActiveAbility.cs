using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "ActiveAbility", menuName = "Spell/ActiveAbility")]
    public class ActiveAbility : Ability
    {
        public AnimationClip animationClip;
    }
}