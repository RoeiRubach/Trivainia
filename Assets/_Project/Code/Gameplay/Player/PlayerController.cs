using UnityEngine;

namespace Trivainia
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerDiMock _di;
        private PlayerStateMachineConfigurator _stateMachine;

        private void Awake()
        {
            _di = GetComponent<PlayerDiMock>();
            _stateMachine = new PlayerStateMachineConfigurator();
        }

        private void Start()
        {
            _stateMachine.Setup(_di);
            _di.Input.EnableActions();
        }

        private void Update() => _stateMachine.Update();
        private void FixedUpdate() => _stateMachine.FixedUpdate();
    }

    public class PlayerStateMachineConfigurator
    {
        private readonly StateMachine _stateMachine = new();

        public void Update() => _stateMachine.Update();
        public void FixedUpdate() => _stateMachine.FixedUpdate();

        public void Setup(PlayerDiMock mock)
        {
            var idleState = new IdleState();
            var movingState = new MovingState(mock.RbRef, mock.Input, mock.MovementProperties, mock.TimeService);

            At(idleState, movingState, new FuncPredicate(() => mock.Input.Direction != Vector3.zero));
            At(movingState, idleState, new FuncPredicate(() => mock.Input.Direction == Vector3.zero));

            _stateMachine.SetState(idleState);
        }

        private void At(IState from, IState to, IPredicateStrategy condition) => _stateMachine.AddTransition(from, to, condition);
        private void Any(IState to, IPredicateStrategy condition) => _stateMachine.AddGlobalTransition(to, condition);
    }
}