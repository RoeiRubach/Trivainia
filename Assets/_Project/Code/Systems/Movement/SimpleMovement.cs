using System;
using Trivainia.Utilities;
using UnityEngine;

namespace Trivainia
{
    public class SimpleMovement : IMovementService
    {
        public event Action<Vector3> FinalVelocityComputed;
        public event Action<Quaternion> RotationComputed;

        private const float PHYSICS_MULTIPLIER = 100f;
        private readonly MovementConfigSO _config;
        private readonly ITimeService _timeService;

        public SimpleMovement(ITimeService timeService, MovementConfigSO config)
        {
            _config = config;
            _timeService = timeService;
        }

        private float PhysicsMoveSpeed => _config.MoveSpeed * PHYSICS_MULTIPLIER;
        private float PhysicsRotationSpeed => _config.RotationSpeed * PHYSICS_MULTIPLIER;
        private float PhysicsAcceleration => _config.Acceleration * PHYSICS_MULTIPLIER;
        private float PhysicsDeceleration => _config.Deceleration * PHYSICS_MULTIPLIER;

        public Vector3 GetZeroVelocity() => Vector3.zero;

        public void ApplyExternalVelocity(Vector3 velocity) => FinalVelocityComputed?.Invoke(velocity);

        public Vector3 ComputeSmoothedVelocity(Vector3 direction, Vector3 currentVelocity)
        {
            var targetVelocity = direction * (PhysicsMoveSpeed * _timeService.GetFixedDeltaTime());

            var acceleration = targetVelocity == Vector3.zero
                ? PhysicsDeceleration
                : PhysicsAcceleration;

            var smoothedVelocity = Vector3.MoveTowards(
                currentVelocity,
                targetVelocity,
                acceleration * _timeService.GetFixedDeltaTime()
            );

            FinalVelocityComputed?.Invoke(smoothedVelocity);

            if (smoothedVelocity.sqrMagnitude < 0.0001f)
                smoothedVelocity = Vector3.zero;

            return smoothedVelocity;
        }

        public Quaternion ComputeSmoothRotation(Quaternion currentRotation, Vector3 direction)
        {
            if (direction == Vector3.zero)
                return currentRotation;

            var targetRotation = Quaternion.LookRotation(direction);

            var smoothedRotation = Quaternion.RotateTowards(
                currentRotation,
                targetRotation,
                PhysicsRotationSpeed * _timeService.GetDeltaTime()
            );

            RotationComputed?.Invoke(smoothedRotation);

            return smoothedRotation;
        }
    }
}