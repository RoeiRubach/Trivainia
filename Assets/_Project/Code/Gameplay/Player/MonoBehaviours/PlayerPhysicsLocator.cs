using Sirenix.OdinInspector;
using UnityEngine;

namespace Trivainia
{
    public class PlayerPhysicsLocator : MonoBehaviour
    {
        [SerializeField, Required] private Rigidbody _rigidbody;
        [SerializeField, Required] private PlayerServiceLocator _locator;
        [ShowInInspector, ReadOnly] private IPhysicsApplier _physicsApplier;

        private void Start() => _physicsApplier ??= new RigidbodyApplier(_rigidbody, _locator.TimeService, _locator.MovementService);
    }
}