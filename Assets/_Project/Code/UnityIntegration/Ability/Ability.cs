namespace Trivainia
{
    public class Ability
    {
        public readonly AbilityDataSO DataSo;

        public Ability(AbilityDataSO dataSo) => DataSo = dataSo;

        public AbilityCommand CreateCommand() => new(DataSo);
    }
}