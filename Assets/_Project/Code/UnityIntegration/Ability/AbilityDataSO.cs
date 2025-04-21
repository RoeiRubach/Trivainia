using Sirenix.OdinInspector;
using UnityEngine;

namespace Trivainia
{
    [CreateAssetMenu(fileName = "AbilityDataSO", menuName = "Scriptable Objects/AbilityData")]
    public class AbilityDataSO : AnimationClipSO
    {
        [VerticalGroup("row1/left")] public float Duration;
        [VerticalGroup("row1/left")] public float CoyoteThreshold = 0.25f;
        [VerticalGroup("row1/left")] public AbilityExecutionStrategySO ExecutionStrategy;

        [HorizontalGroup("row1", 50), VerticalGroup("row1/right"), PreviewField(50, ObjectFieldAlignment.Right), HideLabel]
        public Sprite Icon;
    }
}