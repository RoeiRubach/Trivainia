using Trivainia.Utilities;
using UnityEngine;
using UnityUtils;

namespace Trivainia
{
    public class MovingState : BaseState
    {
        private readonly IMovementService _movement;
        private readonly IInputReader _input;
        private readonly Rigidbody _rb;
        private readonly ITimeService _timeService;

        private Vector3 _currentVelocity;

        public MovingState(
            Rigidbody rb,
            IInputReader input,
            MovementPropertiesSO config,
            ITimeService timeService)
        {
            _rb = rb;
            _input = input;
            _timeService = timeService;
            _movement = new SimpleMovement(timeService, config);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            
            var inputDirection = _input.Direction.With(y: 0);
            var targetVelocity = _movement.ComputeLocomotion(inputDirection);
            _currentVelocity = _movement.SmoothVelocity(_currentVelocity, targetVelocity);

            var newPosition = _rb.position + _currentVelocity * _timeService.GetFixedDeltaTime();
            _rb.MovePosition(newPosition);
        }
    }
}