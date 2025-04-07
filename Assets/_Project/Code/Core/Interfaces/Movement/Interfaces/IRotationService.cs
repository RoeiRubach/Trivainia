using System;
using UnityEngine;

namespace Trivainia
{
    public interface IRotationService
    {
        public Quaternion ComputeRotation(Quaternion currentRotation, Vector3 direction);
    }
}