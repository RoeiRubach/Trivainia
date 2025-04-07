using Sirenix.OdinInspector;
using UnityEngine;

namespace Trivainia
{
    [CreateAssetMenu(fileName = "MovementPropertiesSO", menuName = "Scriptable Objects/MovementPropertiesSO")]
    public class MovementPropertiesSO : ScriptableObject
    {
        [BoxGroup("Speed Settings")]
        [LabelText("Movement Speed")]
        public float MoveSpeed = 5f;
        //public float SprintSpeed = 8f;
        
        [BoxGroup("Speed Settings")]
        [LabelText("Rotation Speed")]
        public float RotationSpeed = 5f;
        
        [BoxGroup("Acceleration Settings")]
        [LabelText("Acceleration")]
        public float Acceleration = 10f;
        [BoxGroup("Acceleration Settings")]
        [LabelText("Deceleration")]
        public float Deceleration = 10f;
    }
}
