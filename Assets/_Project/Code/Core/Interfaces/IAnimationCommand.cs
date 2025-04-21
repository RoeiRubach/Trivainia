namespace Trivainia
{
    public interface IAnimationCommand
    {
        public event System.Action<int> AnimationHashRaised;
    }
}