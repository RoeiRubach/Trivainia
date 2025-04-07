using UnityEngine;

namespace Trivainia
{
    [CreateAssetMenu(fileName = "MovementPropertiesSO", menuName = "Scriptable Objects/MovementPropertiesSO")]
    public class MovementPropertiesSO : ScriptableObject
    {
        [Header("Speed Settings")]
        public float MoveSpeed = 5f;
        //public float SprintSpeed = 8f;
        public float RotationSpeed = 5f;
        
        [Header("Acceleration Settings")]
        public float Acceleration = 10f;
        public float Deceleration = 10f;
    }
}
