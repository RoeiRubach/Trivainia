using Trivainia.Utilities;
using UnityEngine;

namespace Trivainia
{
    public class PlayerServiceLocator : MonoBehaviour
    {
        public IInputReader Input { get; private set; }
        public Transform MainCamera { get; private set; }
        public ITimeService TimeService { get; private set; }
        public IMovementService MovementService { get; private set; }

        [field: SerializeField] public PlayerAnimationsLocator Animations { get; private set; }
        [field: SerializeField] public MovementConfigSO MovementConfig { get; private set; }
        [SerializeField] private ScriptableObject _inputReaderSO;

        private void OnValidate()
        {
            if (_inputReaderSO != null && ValidateInputReader(_inputReaderSO))
                ConsoleLogger.PrintError("Must implement IInputReader. Drag a ScriptableObject that implements IInputReader - Assets/_Project/Code/ScriptableObjects/Configs");
        }

        private void Awake()
        {
            MainCamera = Camera.main.transform;
            TimeService = new UnityTime();
            Input = _inputReaderSO as IInputReader;
            MovementService = new SimpleMovement(TimeService, MovementConfig);
        }

        private static bool ValidateInputReader(ScriptableObject so) => so is IInputReader;
    }
}