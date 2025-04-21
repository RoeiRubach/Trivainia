namespace Trivainia
{
    public class AbilityCommand : IAnimationCommand
    {
        public event System.Action<int> AnimationHashRaised = delegate { };
        private readonly AbilityDataSO _dataSo;
        public float CooldownDuration => _dataSo.CooldownDuration;

        public AbilityCommand(AbilityDataSO dataSo) => _dataSo = dataSo;

        public void Execute()
        {
            _dataSo.ExecutionStrategy?.Execute();
            AnimationHashRaised.Invoke(_dataSo.AnimationHash);
        }
    }
}