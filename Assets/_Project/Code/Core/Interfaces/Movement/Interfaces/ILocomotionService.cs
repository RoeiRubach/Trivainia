using System;
using UnityEngine;

namespace Trivainia
{
    public interface ILocomotionService
    {
        public Vector3 ComputeLocomotion(Vector3 direction);
        public Vector3 SmoothVelocity(Vector3 currentVelocity, Vector3 targetVelocity);
        public Vector3 GetZeroVelocity();
    }
}