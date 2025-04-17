namespace Trivainia
{
    public class AbilityCommand
    {
        public event System.Action<int> AnimationHashRaised = delegate { };
        private readonly AbilityDataSO _dataSo;
        public float Duration => _dataSo.Duration;

        public AbilityCommand(AbilityDataSO dataSo) => _dataSo = dataSo;

        public void Execute() => AnimationHashRaised.Invoke(_dataSo.AnimationHash);
    }
}