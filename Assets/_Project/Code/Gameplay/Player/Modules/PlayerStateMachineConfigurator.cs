using UnityEngine;

namespace Trivainia
{
    public class PlayerStateMachineConfigurator
    {
        private readonly StateMachine _stateMachine = new();

        public void Update() => _stateMachine.Update();
        public void FixedUpdate() => _stateMachine.FixedUpdate();

        public void Setup(PlayerServiceLocator locator, PlayerAnimatorController animator)
        {
            // Contexts
            var idleAnimationContext = new AnimationContext(locator.Animations.Idle, animator);
            var movingAnimationContext = new AnimationContext(locator.Animations.Moving, animator);
            var movementContext = new MovementContext(locator.Input, locator.MovementService, locator.MainCamera);

            // States
            var idleState = new IdleState(idleAnimationContext);
            var movingState = new MovingState(movementContext, movingAnimationContext);

            // Transitions
            At(idleState, movingState, new FuncPredicate(() => locator.Input.Direction != Vector3.zero));
            At(movingState, idleState, new FuncPredicate(() => movingState.CurrentVelocity == Vector3.zero));

            _stateMachine.SetState(idleState);
        }

        private void Any(IState to, IPredicateStrategy condition) => _stateMachine.AddGlobalTransition(to, condition);
        private void At(IState from, IState to, IPredicateStrategy condition) => _stateMachine.AddTransition(from, to, condition);
    }
}
