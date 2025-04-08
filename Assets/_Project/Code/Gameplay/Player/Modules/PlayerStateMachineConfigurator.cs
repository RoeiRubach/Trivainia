using UnityEngine;

namespace Trivainia
{
    public class PlayerStateMachineConfigurator
    {
        private readonly StateMachine _stateMachine = new();

        public void Update() => _stateMachine.Update();
        public void FixedUpdate() => _stateMachine.FixedUpdate();

        public void Setup(PlayerServiceLocator locator)
        {
            var idleState = new IdleState();
            var movingState = new MovingState(locator.Input, locator.MovementService, locator.MainCamera);

            At(idleState, movingState, new FuncPredicate(() => locator.Input.Direction != Vector3.zero));
            At(movingState, idleState, new FuncPredicate(() => locator.Input.Direction == Vector3.zero));

            _stateMachine.SetState(idleState);
        }

        private void Any(IState to, IPredicateStrategy condition) => _stateMachine.AddGlobalTransition(to, condition);
        private void At(IState from, IState to, IPredicateStrategy condition) => _stateMachine.AddTransition(from, to, condition);
    }
}