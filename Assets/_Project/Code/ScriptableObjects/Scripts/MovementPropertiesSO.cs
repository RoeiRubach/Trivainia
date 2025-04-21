using Sirenix.OdinInspector;
using UnityEngine;

namespace Trivainia
{
    [CreateAssetMenu(fileName = "MovementPropertiesSO", menuName = "Scriptable Objects/MovementPropertiesSO")]
    public class MovementPropertiesSO : ScriptableObject
    {
        [BoxGroup("Speed Settings"), MinValue(1)]
        public float MoveSpeed = 5f;

        //public float SprintSpeed = 8f;
        [BoxGroup("Speed Settings"), MinValue(1)]
        public float RotationSpeed = 5f;

        [BoxGroup("Acceleration Settings"), PropertyRange(0.001f, 1f)]
        public float Acceleration = 1f;

        [BoxGroup("Acceleration Settings"), PropertyRange(0.001f, 1f)]
        public float Deceleration = 1f;
    }
}