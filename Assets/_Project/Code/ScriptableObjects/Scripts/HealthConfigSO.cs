using Sirenix.OdinInspector;
using UnityEngine;

namespace Trivainia
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Health Config")]
    public class HealthConfigSO : ScriptableObject
    {
        [SerializeField, MinValue(1)] private float _initialValue;
        [SerializeField, MinValue(1)] private float _maxValue;

        private void OnValidate()
        {
            if (_maxValue < _initialValue)
                _maxValue = _initialValue;
        }

        public float InitialValue => _initialValue;
        public float MaxValue => _maxValue;
    }
}