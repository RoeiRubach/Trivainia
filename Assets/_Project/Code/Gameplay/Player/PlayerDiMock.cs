using Sirenix.OdinInspector;
using Trivainia.Utilities;
using UnityEngine;

namespace Trivainia
{
    public class PlayerDiMock : MonoBehaviour
    {
        public Rigidbody RbRef;

        [ValidateInput(nameof(ValidateInputReader), "Must implement IInputReader"),
         Tooltip("Drag a ScriptableObject that implements IInputReader"),
         SerializeField, AssetSelector(Paths = "Assets/_Project/Code/ScriptableObjects/Configs")]
        private ScriptableObject _inputReaderSO;

        public MovementPropertiesSO MovementProperties;

        public ITimeService TimeService;
        public IInputReader Input { get; private set; }

        private void Awake()
        {
            TimeService = new UnityTime();
            Input = _inputReaderSO as IInputReader;
        }

        private bool ValidateInputReader(ScriptableObject so) => so is IInputReader;
    }
}