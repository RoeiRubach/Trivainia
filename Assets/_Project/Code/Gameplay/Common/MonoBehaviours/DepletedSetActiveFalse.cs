using UnityEngine;

namespace Trivainia
{
    public class DepletedSetActiveFalse : MonoBehaviour
    {
        private IResourceManageable _resource;

        private void Start() => _resource.Depleted += OnDepleted;

        private void OnDepleted() => gameObject.SetActive(false);

        private void OnDestroy() => _resource.Depleted -= OnDepleted;
    }
}