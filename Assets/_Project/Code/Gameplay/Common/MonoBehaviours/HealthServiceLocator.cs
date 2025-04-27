using UnityEngine;

namespace Trivainia
{
    public class HealthServiceLocator : MonoBehaviour
    {
        [SerializeField] private HealthConfigSO _healthConfig;

        public IHealthService GetNewHealthService() => new HealthService(_healthConfig);
    }
}