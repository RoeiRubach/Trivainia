using System.Collections.Generic;
using Trivainia.Utilities;

namespace Trivainia
{
    public interface IState
    {
        public bool IsComposite { get; }
        public void OnEnter();
        public void Update();
        public void FixedUpdate();
        public void OnExit();
    }

    public interface ICompositeState : IState
    {
        public HashSet<IState> SubStates { get; }

        bool IState.IsComposite => true;

        public void AddSubState(IState state);
    }

    public abstract class BaseState : IState
    {
        public bool IsComposite => false;

        public virtual void OnEnter() => ConsoleLogger.Print($"Enter {GetType().Name}");

        public virtual void Update() { }

        public virtual void FixedUpdate() { }

        public virtual void OnExit() { }
    }
}