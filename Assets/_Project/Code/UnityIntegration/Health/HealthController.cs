using Sirenix.OdinInspector;
using UnityEngine;

namespace Trivainia
{
    public class HealthController : MonoBehaviour
    {
        [SerializeField, Required] private HealthView _healthView;
        private IHealthService _health;

        private void Start()
        {
            _health = FindAnyObjectByType<HealthServiceLocator>().GetNewHealthService();
            _healthView.Initialize(_health);
        }

        public void Deplete(float amount)
        {
            _health.Deplete(amount);
        }

        public void Restore(float amount)
        {
            _health.Restore(amount);
        }
    }
}