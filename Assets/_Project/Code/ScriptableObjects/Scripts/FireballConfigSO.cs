using UnityEngine;

namespace Trivainia
{
    [CreateAssetMenu(fileName = "FireballConfigSO", menuName = "Scriptable Objects/FireballConfigSO")]
    public class FireballConfigSO : DamageConfigSO
    {
        [field: SerializeField, Min(1)] public int MaxTargets { get; private set; }
        [field: SerializeField, Min(5)] public float LifeTime { get; private set; }
        [field: SerializeField, Min(5)] public float MoveSpeed { get; private set; }
        [field: SerializeField] public LayerMask TargetingLayer { get; private set; }
    }
}