using UnityEngine;

namespace Trivainia
{
    public class MovingState : AnimationStateEnter
    {
        private readonly Transform _mainCamera;
        private readonly IInputReader _input;
        private readonly IMovementService _movement;

        public Vector3 CurrentVelocity { get; private set; }
        private Quaternion _currentRotation;

        public MovingState(MovementContext movementContext, AnimationContext animationContext) : base(animationContext)
        {
            _input = movementContext.Input;
            _movement = movementContext.Movement;
            _mainCamera = movementContext.MainCamera;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            var inputDirection = GetInputDirection();
            var worldDirection = GetCameraAdjustedDirection(inputDirection);

            UpdateRotation(worldDirection);
            UpdateVelocity(worldDirection);
        }

        private Vector3 GetInputDirection()
        {
            var input = _input.Direction;

            return new Vector3(input.x, 0f, input.y);
        }

        private Vector3 GetCameraAdjustedDirection(Vector3 direction)
        {
            var cameraYaw = _mainCamera.eulerAngles.y;
            var cameraRotation = Quaternion.AngleAxis(cameraYaw, Vector3.up);

            return cameraRotation * direction;
        }

        private void UpdateRotation(Vector3 direction) => _currentRotation = _movement.ComputeSmoothRotation(_currentRotation, direction);

        private void UpdateVelocity(Vector3 direction) => CurrentVelocity = _movement.ComputeSmoothedVelocity(direction, CurrentVelocity);
    }
}