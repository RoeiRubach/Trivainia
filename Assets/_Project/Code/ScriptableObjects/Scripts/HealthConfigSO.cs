using UnityEngine;

namespace Trivainia
{
    [CreateAssetMenu(menuName = "Game/Health Config")]
    public class HealthConfigSO : ScriptableObject
    {
        [SerializeField] private float _initialValue;
        [SerializeField] private float _maxValue;

        public float InitialValue => _initialValue;
        public float MaxValue => _maxValue;
    }
}