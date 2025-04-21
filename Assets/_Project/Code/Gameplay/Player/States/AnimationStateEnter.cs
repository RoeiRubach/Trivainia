namespace Trivainia
{
    public class AnimationStateEnter : BaseState, IAnimationCommand
    {
        public event System.Action<int> AnimationHashRaised = delegate { };
        private readonly AnimationClipSO _animation;

        protected AnimationStateEnter(AnimationContext animationContext)
        {
            _animation = animationContext.Animation;
            animationContext.Animator.RegisterCommand(this);
        }

        public override void OnEnter()
        {
            base.OnEnter();
            AnimationHashRaised.Invoke(_animation.AnimationHash);
        }
    }
}