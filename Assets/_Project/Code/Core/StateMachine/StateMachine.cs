using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivainia
{
    public sealed class StateMachine
    {
        private StateNode _activeNode;
        private readonly List<StateTransition> _globalTransitions = new();
        private readonly Dictionary<Type, StateNode> _stateNodes = new();

        public void Update()
        {
            var validTransition = FindValidTransition();
            if (validTransition != null)
                SwitchState(validTransition.To);

            _activeNode.State?.Update();
        }

        public void FixedUpdate() => _activeNode.State?.FixedUpdate();

        public void SetState(IState state)
        {
            _activeNode = _stateNodes[state.GetType()];
            _activeNode.State?.OnEnter();
        }

        public void AddGlobalTransition(IState to, IPredicateStrategy condition) => _globalTransitions.Add(new StateTransition(GetOrCreateNode(to).State, condition));
        public void AddTransition(IState from, IState to, IPredicateStrategy condition) => GetOrCreateNode(from).AddTransition(GetOrCreateNode(to).State, condition);

        private StateTransition FindValidTransition()
        {
            foreach (var transition in _globalTransitions.Where(transition => transition.Condition.Evaluate()))
                return transition;

            return _activeNode.Transitions.FirstOrDefault(transition => transition.Condition.Evaluate());
        }

        private void SwitchState(IState state)
        {
            if (IsSameState(state))
                return;

            var previousState = _activeNode;
            var nextNode = _stateNodes[state.GetType()];
            var nextState = nextNode.State;

            previousState.State?.OnExit();
            nextState.OnEnter();
            _activeNode = nextNode;
        }

        private bool IsSameState(IState state)
        {
            if (!_activeNode.State.IsComposite)
                return _activeNode.State == state;

            return AreDeepestStatesEqual(_activeNode.State, state);
        }

        private static bool AreDeepestStatesEqual(IState rootState, IState targetState)
        {
            var composite = rootState as ICompositeState;
            var subStates = composite.SubStates;

            foreach (var subState in subStates)
                if (subState.IsComposite)
                {
                    if (AreDeepestStatesEqual(subState, targetState))
                        return true;
                }
                else if (subState == targetState)
                {
                    return true;
                }

            return false;
        }

        private StateNode GetOrCreateNode(IState state)
        {
            if (!_stateNodes.TryGetValue(state.GetType(), out var node))
            {
                node = new StateNode(state);
                _stateNodes[state.GetType()] = node;
            }

            return node;
        }

        private class StateNode
        {
            public IState State { get; }
            public HashSet<StateTransition> Transitions { get; }

            public StateNode(IState state)
            {
                State = state;
                Transitions = new HashSet<StateTransition>();
            }

            public void AddTransition(IState to, IPredicateStrategy condition) => Transitions.Add(new StateTransition(to, condition));
        }
    }
}