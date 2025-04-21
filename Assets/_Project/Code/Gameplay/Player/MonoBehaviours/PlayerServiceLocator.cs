using Sirenix.OdinInspector;
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

        [field: SerializeField, Required] public PlayerAnimationsLocator Animations { get; private set; }
        [field: SerializeField, Required] public MovementPropertiesSO MovementProperties { get; private set; }

        [ValidateInput(nameof(ValidateInputReader), "Must implement IInputReader"),
         Tooltip("Drag a ScriptableObject that implements IInputReader"),
         SerializeField, AssetSelector(Paths = "Assets/_Project/Code/ScriptableObjects/Configs")]
        private ScriptableObject _inputReaderSO;

        private void Awake()
        {
            MainCamera = Camera.main.transform;
            TimeService = new UnityTime();
            Input = _inputReaderSO as IInputReader;
            MovementService = new SimpleMovement(TimeService, MovementProperties);
        }

        private bool ValidateInputReader(ScriptableObject so) => so is IInputReader;
    }
}