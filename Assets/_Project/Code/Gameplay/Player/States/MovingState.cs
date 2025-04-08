using UnityEngine;

namespace Trivainia
{
    public class MovingState : BaseState
    {
        private readonly Transform _mainCamera;
        private readonly IInputReader _input;
        private readonly IMovementService _movement;

        private Vector3 _currentVelocity;
        private Quaternion _currentRotation;

        public MovingState(IInputReader input, IMovementService movement, Transform mainCamera)
        {
            _input = input;
            _movement = movement;
            _mainCamera = mainCamera;
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

        private void UpdateVelocity(Vector3 direction)
        {
            _currentVelocity = _movement.ComputeSmoothedVelocity(direction, _currentVelocity);
        }
    }
}