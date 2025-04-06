using System;
using System.Collections.Generic;

namespace Trivainia
{
    public class StateMachine
    {
        private readonly HashSet<IState> _activeStates = new();
        private readonly List<Transition> _transitions = new();
        private readonly Dictionary<Type, StateNode> _nodes = new();

        public void Update()
        {
            var transition = GetTransition();
            if (transition != null)
                ChangeState(transition.To);

            foreach (var state in _activeStates)
                state?.Update();
        }

        public void FixedUpdate()
        {
            foreach (var state in _activeStates)
                state?.FixedUpdate();
        }

        public void SetState(IState state)
        {
            currentNode = nodes[state.GetType()];
            currentNode.State?.OnEnter();
        }

        public void AddGlobalTransition(IState to, IPredicateStrategy condition) { }

        public void AddTransition(IState from, IState to, IPredicateStrategy condition) { }

        private void ProcessTransitions() { }

        private void ChangeState(IState state)
        {
            if (state == currentNode.State)
                return;

            var previousState = currentNode.State;
            var nextState = nodes[state.GetType()].State;

            previousState?.OnExit();
            nextState.OnEnter();
            currentNode = nodes[state.GetType()];
        }

        private StateNode GetOrAddNode(IState state)
        {
            
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