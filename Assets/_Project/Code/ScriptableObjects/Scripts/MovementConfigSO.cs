using UnityEngine;

namespace Trivainia
{
    [CreateAssetMenu(fileName = "MovementPropertiesSO", menuName = "Scriptable Objects/MovementPropertiesSO")]
    public class MovementConfigSO : ScriptableObject
    {
        [Min(1)] public float MoveSpeed = 5f;
        [Min(1)] public float RotationSpeed = 5f;
        [Range(0.001f, 1f)] public float Acceleration = 1f;
        [Range(0.001f, 1f)] public float Deceleration = 1f;
    }
}