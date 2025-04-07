using Trivainia.Utilities;
using UnityEngine;

namespace Trivainia
{
    public class SimpleMovement : IMovementService
    {
        private const float PHYSICS_MULTIPLIER = 100f;
        private readonly MovementPropertiesSO _config;
        private readonly ITimeService _timeService;

        public SimpleMovement(ITimeService timeService, MovementPropertiesSO config)
        {
            _config = config;
            _timeService = timeService;
        }

        private float PhysicsMoveSpeed => _config.MoveSpeed * PHYSICS_MULTIPLIER;
        private float PhysicsAcceleration => _config.Acceleration * PHYSICS_MULTIPLIER;
        private float PhysicsDeceleration => _config.Deceleration * PHYSICS_MULTIPLIER;

        public Vector3 GetZeroVelocity() => Vector3.zero;

        public Vector3 ComputeLocomotion(Vector3 direction) => direction * (PhysicsMoveSpeed * _timeService.GetFixedDeltaTime());

        public Vector3 SmoothVelocity(Vector3 currentVelocity, Vector3 targetVelocity)
        {
            var accel = targetVelocity == Vector3.zero
                ? PhysicsDeceleration
                : PhysicsAcceleration;

            return Vector3.MoveTowards(
                currentVelocity,
                targetVelocity,
                accel * _timeService.GetFixedDeltaTime()
            );
        }

        public Quaternion ComputeRotation(Quaternion currentRotation, Vector3 direction)
        {
            if (direction == Vector3.zero)
                return currentRotation;

            var targetRotation = Quaternion.LookRotation(direction);

            return Quaternion.RotateTowards(
                currentRotation,
                targetRotation,
                _config.RotationSpeed * _timeService.GetDeltaTime()
            );
        }
    }
}