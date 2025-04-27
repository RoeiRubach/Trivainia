using UnityEngine;

namespace Trivainia
{
    public class PlayerPhysicsLocator : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private PlayerServiceLocator _locator;
        private IPhysicsApplier _physicsApplier;

        private void Start() => _physicsApplier ??= new RigidbodyApplier(_rigidbody, _locator.TimeService, _locator.MovementService);
    }
}