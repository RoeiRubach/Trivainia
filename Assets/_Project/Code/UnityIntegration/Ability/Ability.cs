namespace Trivainia
{
    public class Ability
    {
        public readonly AbilityDataSO DataSo;
        private AbilityCommand _command;

        public Ability(AbilityDataSO dataSo, PlayerAnimatorController animator)
        {
            DataSo = dataSo;
            animator.RegisterCommand(GetOrCreateCommand());
        }

        public AbilityCommand GetOrCreateCommand()
        {
            _command ??= new AbilityCommand(DataSo);
            return _command;
        }
    }
}