namespace Trivainia
{
    public class MovementContext
    {
        public IInputReader Input { get; }
        public IMovementService Movement { get; }
        public UnityEngine.Transform MainCamera { get; }

        public MovementContext(IInputReader input, IMovementService movement, UnityEngine.Transform mainCamera)
        {
            Input = input;
            Movement = movement;
            MainCamera = mainCamera;
        }
    }
}