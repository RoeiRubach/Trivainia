using UnityEngine;

namespace Trivainia
{
    public interface IRotationService
    {
        public event System.Action<Quaternion> RotationComputed; 
        public Quaternion ComputeRotation(Quaternion currentRotation, Vector3 direction);
    }
}