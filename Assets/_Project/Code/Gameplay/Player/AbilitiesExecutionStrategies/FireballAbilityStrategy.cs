using UnityEngine;

namespace Trivainia
{
    [CreateAssetMenu(fileName = "FireballAbilityStrategy", menuName = "Scriptable Objects/Abilities/FireballAbilityStrategy")]
    public class FireballAbilityStrategy : AbilityExecutionStrategySO
    {
        [SerializeField] private GameObject _fireballPrefab;
        public float SearchRadius = 15f;
        [SerializeField] private LayerMask _targetingLayer;

        private readonly Collider[] _targetBuffer = new Collider[50];

        public override void Execute()
        {
            var originPosition = Origin.position;
            var targetPosition = GetClosestTargetPosition(originPosition);

            var fireball = Instantiate(_fireballPrefab, originPosition, Quaternion.identity).GetComponentInChildren<FireballController>();
            fireball.Initialize(targetPosition);
        }

        private Vector3 GetClosestTargetPosition(Vector3 origin)
        {
            var hitCount = Physics.OverlapSphereNonAlloc(origin, SearchRadius, _targetBuffer, _targetingLayer);

            var closestDistance = Mathf.Infinity;
            Transform closestTarget = null;

            for (var i = 0; i < hitCount; i++)
            {
                var target = _targetBuffer[i].transform;
                var dist = Vector3.Distance(origin, target.position);

                if (!(dist < closestDistance))
                    continue;
                closestDistance = dist;
                closestTarget = target;
            }

            var shootForward = origin + Origin.forward * 10f;

            return closestTarget != null
                ? closestTarget.position
                : shootForward;
        }
    }
}