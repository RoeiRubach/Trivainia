using UnityEngine;

namespace Trivainia
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Health Config")]
    public class HealthConfigSO : ScriptableObject
    {
        [SerializeField, Min(1)] private float _initialValue;
        [SerializeField, Min(1)] private float _maxValue;

        private void OnValidate()
        {
            if (_maxValue < _initialValue)
                _maxValue = _initialValue;
        }

        public float InitialValue => _initialValue;
        public float MaxValue => _maxValue;
    }
}