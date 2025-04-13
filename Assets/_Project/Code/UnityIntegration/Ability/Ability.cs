namespace Trivainia
{
    public class Ability
    {
        public readonly AbilityData Data;

        public Ability(AbilityData data) => Data = data;

        public AbilityCommand CreateCommand() => new(Data);
    }
}