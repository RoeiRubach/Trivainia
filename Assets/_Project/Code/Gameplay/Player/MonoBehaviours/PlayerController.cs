using UnityEngine;

namespace Trivainia
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerServiceLocator _locator;
        private PlayerStateMachineConfigurator _stateMachine;

        private void Awake()
        {
            _locator = GetComponent<PlayerServiceLocator>();
            _stateMachine = new PlayerStateMachineConfigurator();
        }

        private void Start()
        {
            _stateMachine.Setup(_locator);
            _locator.Input.EnableActions();
        }

        private void Update() => _stateMachine.Update();
        private void FixedUpdate() => _stateMachine.FixedUpdate();
    }
}