using UnityEngine;

namespace Trivainia
{
    public interface ILocomotionService
    {
        public event System.Action<Vector3> FinalVelocityComputed;
        public Vector3 ComputeSmoothedVelocity(Vector3 currentVelocity, Vector3 targetVelocity);
        public Vector3 GetZeroVelocity();
    }
}