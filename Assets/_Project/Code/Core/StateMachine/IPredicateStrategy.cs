using System;

namespace Trivainia
{
    public interface IPredicateStrategy
    {
        public bool Evaluate();
    }

    public class FuncPredicate : IPredicateStrategy
    {
        private readonly Func<bool> _func;
        public FuncPredicate(Func<bool> func) => _func = func;
        public bool Evaluate() => _func.Invoke();
    }
}