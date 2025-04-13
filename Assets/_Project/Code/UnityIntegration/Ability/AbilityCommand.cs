namespace Trivainia
{
    public class AbilityCommand
    {
        public event System.Action<int> AnimationHashRaised = delegate { };
        private readonly AbilityData _data;
        public float Duration => _data.Duration;

        public AbilityCommand(AbilityData data) => _data = data;

        public void Execute() => AnimationHashRaised.Invoke(_data.AnimationHash);
    }
}