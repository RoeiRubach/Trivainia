namespace Trivainia
{
    public sealed class StateTransition
    {
        public IState To { get; }
        public IPredicateStrategy Condition { get; }
        
        public StateTransition(IState to, IPredicateStrategy condition)
        {
            To = to;
            Condition = condition;
        }
    }
}