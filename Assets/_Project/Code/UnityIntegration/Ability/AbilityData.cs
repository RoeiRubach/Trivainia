using Sirenix.OdinInspector;
using UnityEngine;

namespace Trivainia
{
    [CreateAssetMenu(fileName = "AbilityData", menuName = "Scriptable Objects/AbilityData")]
    public class AbilityData : ScriptableObject
    {
        [VerticalGroup("row1/left")] public AnimationClip AnimationClip;
        [VerticalGroup("row1/left"), ReadOnly] public int AnimationHash;
        [VerticalGroup("row1/left")] public float Duration;

        [HorizontalGroup("row1", 50), VerticalGroup("row1/right"), PreviewField(50, ObjectFieldAlignment.Right), HideLabel]
        public Sprite Icon;

        private void OnValidate()
        {
            if (AnimationClip == null)
                return;
            AnimationHash = Animator.StringToHash(AnimationClip.name);
        }
    }
}