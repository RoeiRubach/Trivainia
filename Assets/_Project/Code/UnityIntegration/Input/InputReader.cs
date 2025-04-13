using System;
using Trivainia.Utilities;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static PlayerInputActions;

namespace Trivainia
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "Scriptable Objects/InputReader")]
    public class InputReader : ScriptableObject, IPlayerActions, IInputReader
    {
        public event UnityAction<Vector2> Move = delegate { };
        private readonly Action[] _skillActions = new Action[4];

        private PlayerInputActions _inputActions;

        private void OnEnable()
        {
            if (_inputActions != null) return;

            _inputActions = new PlayerInputActions();
            _inputActions.Player.SetCallbacks(this);
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