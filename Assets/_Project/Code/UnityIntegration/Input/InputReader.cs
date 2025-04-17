using System;
using Trivainia.Utilities;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using static PlayerInputActions;

namespace Trivainia
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "Scriptable Objects/InputReader")]
    public class InputReader : ScriptableObject, IPlayerActions, IInputReader
    {
        public event UnityAction<Vector2> Move = delegate { };
        private readonly Action[] _skillActions = new Action[4];
        public event Action<InputDeviceType> InputDeviceChanged = delegate { };

        private InputDeviceType _lastUsedInputType = InputDeviceType.KeyboardMouse;

        private PlayerInputActions _inputActions;

        private void OnEnable()
        {
            if (_inputActions != null) return;

            _inputActions = new PlayerInputActions();
            _inputActions.Player.SetCallbacks(this);
            InputSystem.onEvent += OnInputSystemEvent;
        }

        private void OnInputSystemEvent(InputEventPtr eventPtr, InputDevice device)
        {
            if (!eventPtr.IsA<StateEvent>() && !eventPtr.IsA<DeltaStateEvent>())
                return;

            switch (device)
            {
                case Gamepad:
                    TrySetDevice(InputDeviceType.Gamepad);

                    break;
                case Keyboard or Mouse:
                    TrySetDevice(InputDeviceType.KeyboardMouse);

                    break;
            }
        }

        private void TrySetDevice(InputDeviceType newInputType)
        {
            if (_lastUsedInputType == newInputType)
                return;

            _lastUsedInputType = newInputType;
            InputDeviceChanged.Invoke(newInputType);
        }

        public Vector3 Direction => _inputActions.Player.Move.ReadValue<Vector2>();

        public void RegisterSkillAction(int index, Action action)
        {
            if (index < 0 || index >= _skillActions.Length)
            {
                ConsoleLogger.PrintWarning($"Trying to register skill action at invalid index: {index}");

                return;
            }

            _skillActions[index] += action;
        }

        public void EnableActions() => _inputActions.Enable();

        public void OnMove(InputAction.CallbackContext context) => Move.Invoke(context.ReadValue<Vector2>());

        public void OnSkillBottom(InputAction.CallbackContext context)
        {
            if (context.performed)
                _skillActions[0].Invoke();
        }

        public void OnSkillLeft(InputAction.CallbackContext context)
        {
            if (context.performed)
                _skillActions[1].Invoke();
        }

        public void OnSkillTop(InputAction.CallbackContext context)
        {
            if (context.performed)
                _skillActions[2].Invoke();
        }

        public void OnSkillRight(InputAction.CallbackContext context)
        {
            if (context.performed)
                _skillActions[3].Invoke();
        }
    }
}