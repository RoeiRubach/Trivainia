using Sirenix.OdinInspector;
using UnityEngine;

namespace Trivainia
{
    [CreateAssetMenu(fileName = "AnimationClipSO", menuName = "Scriptable Objects/AnimationClip")]
    public class AnimationClipSO : ScriptableObject
    {
        public AnimationClip AnimationClip;
        public int AnimationHash;

        private void OnValidate()
        {
            if (AnimationClip == null)
                return;
            AnimationHash = Animator.StringToHash(AnimationClip.name);
        }
    }
}