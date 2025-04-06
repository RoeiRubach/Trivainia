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
        bool IState.IsComposite
        {
            get => true;
        }

        public System.Collections.Generic.HashSet<IState> SubStates { get; }
        public void AddSubState(IState state);
    }
}