using Trivainia.Utilities;
using UnityEngine;

namespace Trivainia
{
    public class RigidbodyApplier : IPhysicsApplier
    {
        private readonly Rigidbody _rigidbody;
        private readonly ITimeService _timeService;

        public Vector3 GetCurrentVelocity => _rigidbody.linearVelocity;

        public RigidbodyApplier(Rigidbody rigidbody, ITimeService timeService, IMovementService movement)
        {
            _rigidbody = rigidbody;
            _timeService = timeService;

            movement.FinalVelocityComputed += OnFinalVelocityComputed;
        }

        private void OnFinalVelocityComputed(Vector3 velocity) => HandleHorizontalMovement(velocity);

        private void HandleHorizontalMovement(Vector3 velocity)
        {
            var newPosition = _rigidbody.position + velocity * _timeService.GetFixedDeltaTime();
            _rigidbody.MovePosition(newPosition);
        }
    }
}