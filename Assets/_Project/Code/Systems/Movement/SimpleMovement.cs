using Trivainia.Utilities;
using UnityEngine;

namespace Trivainia
{
    public class SimpleMovement : IMovementService
    {
        private readonly ITimeService _timeService;
        private readonly MovementPropertiesSO _config;

        public SimpleMovement(ITimeService timeService, MovementPropertiesSO config)
        {
            _config = config;
            _timeService = timeService;
        }
        
        public Vector3 GetZeroVelocity() => Vector3.zero;

        public Vector3 ComputeLocomotion(Vector3 direction) => direction * (_config.MoveSpeed * _timeService.GetFixedDeltaTime());

        public Quaternion ComputeRotation(Quaternion currentRotation, Vector3 direction)
        {
            if (direction == Vector3.zero)
                return currentRotation;
            
            var targetRotation = Quaternion.LookRotation(direction);
            return Quaternion.RotateTowards(currentRotation, targetRotation, _config.RotationSpeed * _timeService.GetDeltaTime());
        }
    }
}
