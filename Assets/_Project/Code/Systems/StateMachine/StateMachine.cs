using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivainia
{
    public class StateMachine
    {
        private StateNode _currentNode;
        private readonly List<Transition> _globalTransitions = new();
        private readonly Dictionary<Type, StateNode> _nodes = new();

        public void Update()
        {
            var transition = ProcessTransitions();
            if (transition != null)
                ChangeState(transition.To);

            _currentNode.State?.Update();
        }

        public void FixedUpdate() => _currentNode.State?.FixedUpdate();

        public void SetState(IState state)
        {
            _currentNode = _nodes[state.GetType()];
            _currentNode.State?.OnEnter();
        }

        public void AddGlobalTransition(IState to, IPredicateStrategy condition) => _globalTransitions.Add(new Transition(GetOrAddNode(to).State, condition));
        public void AddTransition(IState from, IState to, IPredicateStrategy condition) => GetOrAddNode(from).AddTransition(GetOrAddNode(to).State, condition);

        private Transition ProcessTransitions()
        {
            foreach (var transition in _globalTransitions.Where(transition => transition.Condition.Evaluate()))
                return transition;

            return _currentNode.Transitions.FirstOrDefault(transition => transition.Condition.Evaluate());
        }

        private void ChangeState(IState state)
        {
            if (IsTryingToChangeToSameState(state))
                return;

            var previousActiveState = _currentNode;
            var nextState = _nodes[state.GetType()].State;

            previousActiveState.State?.OnExit();
            nextState.OnEnter();
            _currentNode = _nodes[state.GetType()];
        }

        private bool IsTryingToChangeToSameState(IState state)
        {
            if (!_currentNode.State.IsComposite)
                return _currentNode.State == state;

            return CompareDeepestStatesInComposite(_currentNode.State, state);
        }

        private static bool CompareDeepestStatesInComposite(IState rootState, IState targetState)
        {
            var compositeState = rootState as ICompositeState;
            var subStates = compositeState.SubStates;

            foreach (var subState in subStates)
                if (subState.IsComposite)
                {
                    if (CompareDeepestStatesInComposite(subState, targetState))
                        return true;
                }
                else if (subState == targetState)
                {
                    return true;
                }

            return false;
        }

        private StateNode GetOrAddNode(IState state)
        {
            var node = _nodes.GetValueOrDefault(state.GetType());

            if (node == null)
            {
                node = new StateNode(state);
                _nodes[state.GetType()] = node;
            }

            return node;
        }

        private class StateNode
        {
            public IState State { get; }
            public HashSet<Transition> Transitions { get; }

            public StateNode(IState state)
            {
                State = state;
                Transitions = new HashSet<Transition>();
            }

            public void AddTransition(IState to, IPredicateStrategy condition) => Transitions.Add(new Transition(to, condition));
        }
    }
}