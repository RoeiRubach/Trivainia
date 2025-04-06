namespace Trivainia
{
    public sealed class Transition
    {
        public IState To { get;}
        public IPredicateStrategy Condition { get; }

        public Transition(IState to, IPredicateStrategy condition)
        {
            To = to;
            Condition = condition;
        }
    }
}