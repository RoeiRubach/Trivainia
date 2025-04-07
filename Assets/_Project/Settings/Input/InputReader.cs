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
        
        private PlayerInputActions _inputActions;

        private void OnEnable()
        {
            if (_inputActions != null)
                return;
            _inputActions = new PlayerInputActions();
            _inputActions.Player.SetCallbacks(this);
        }
        
        public Vector3 Direction
        {
            get => _inputActions.Player.Move.ReadValue<Vector2>();
        }
        
        public void OnMove(InputAction.CallbackContext context)
        {
            Move.Invoke(context.ReadValue<Vector2>());
        }
    }
}
