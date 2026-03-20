using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "ActiveAbility", menuName = "Ability/ActiveAbility")]
    public class ActiveAbility : Ability
    {
        public AnimationClip animationClip;
    }
}