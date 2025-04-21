using Sirenix.OdinInspector;
using UnityEngine;

namespace Trivainia
{
    [CreateAssetMenu(fileName = "AnimationClipSO", menuName = "Scriptable Objects/AnimationClip")]
    public class AnimationClipSO : ScriptableObject
    {
        [VerticalGroup("row1/left")] public AnimationClip AnimationClip;
        [VerticalGroup("row1/left"), ReadOnly] public int AnimationHash;
        
        private void OnValidate()
        {
            if (AnimationClip == null)
                return;
            AnimationHash = Animator.StringToHash(AnimationClip.name);
        }
    }
}