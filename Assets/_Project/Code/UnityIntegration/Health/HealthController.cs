using Sirenix.OdinInspector;
using UnityEngine;

namespace Trivainia
{
    public class HealthController : MonoBehaviour, IResourceManageable
    {
        [SerializeField, Required] private HealthView _healthView;
        [SerializeField, Required, SceneObjectsOnly] private HealthServiceLocator _locator;
        private IHealthService _health;

        private void Start()
        {
            _health = _locator.GetNewHealthService();
            _healthView.Initialize(_health);
        }

        public void Reset() => _health.Reset();
        public void Deplete(float amount) => _health.Deplete(amount);
        public void Restore(float amount) => _health.Restore(amount);
    }
}