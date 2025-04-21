namespace Trivainia
{
    public class AnimationContext
    {
        public AnimationClipSO Animation;
        public PlayerAnimatorController Animator { get; }

        public AnimationContext(AnimationClipSO animationClip, PlayerAnimatorController animatorController)
        {
            Animation = animationClip;
            Animator = animatorController;
        }
    }
}