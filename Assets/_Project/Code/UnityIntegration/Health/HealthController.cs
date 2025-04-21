using Sirenix.OdinInspector;
using UnityEngine;

namespace Trivainia
{
    public class HealthController : MonoBehaviour, IResourceManageable
    {
        [SerializeField, Required] private HealthView _healthView;
        [SerializeField, Required, SceneObjectsOnly] private HealthServiceLocator _locator;
        private IHealthService _health;
        
        public event System.Action Depleted
        {
            add => _health.Depleted += value;
            remove => _health.Depleted -= value;
        }

        private void Awake()
        {
            _health = _locator.GetNewHealthService();
            _healthView.Initialize(_health);
        }

        public void Reset() => _health.Reset();
        public void Deplete(float amount) => _health.Deplete(amount);
        public void Restore(float amount) => _health.Restore(amount);
    }
}