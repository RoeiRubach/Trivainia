using UnityEngine;

namespace Trivainia
{
    [CreateAssetMenu(fileName = "AbilityDataSO", menuName = "Scriptable Objects/AbilityData")]
    public class AbilityDataSO : AnimationClipSO
    {
        public float CooldownDuration;
        public float CoyoteThreshold = 0.25f;
        public AbilityExecutionStrategySO ExecutionStrategy;

        public Sprite Icon;
    }
}