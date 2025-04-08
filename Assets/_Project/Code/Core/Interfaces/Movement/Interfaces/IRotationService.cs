using UnityEngine;

namespace Trivainia
{
    public interface IRotationService
    {
        public event System.Action<Quaternion> RotationComputed;
        public Quaternion ComputeSmoothRotation(Quaternion currentRotation, Vector3 direction);
    }
}