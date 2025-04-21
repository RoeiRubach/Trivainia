using Sirenix.OdinInspector;
using UnityEngine;

namespace Trivainia
{
    public class HealthServiceLocator : MonoBehaviour
    {
        [SerializeField, Required] private HealthConfigSO _healthConfig;

        public IHealthService GetNewHealthService() => new HealthService(_healthConfig);
    }
}