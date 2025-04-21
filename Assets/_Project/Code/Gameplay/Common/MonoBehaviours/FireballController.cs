using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Trivainia
{
    public class FireballController : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        [SerializeField, Required] private FireballConfigSO _damageConfig;
        private float _hitRadius;

        private Vector3 _direction;
        private bool _hasExploded;

        private static Collider[] _hitResults;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _hitRadius = GetComponent<SphereCollider>().radius;
            _hitResults = new Collider[_damageConfig.MaxTargets];
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_hasExploded) return;
            if (((1 << other.gameObject.layer) & _damageConfig.TargetingLayer) == 0) return;

            _hasExploded = true;

            var hitCount = Physics.OverlapSphereNonAlloc(transform.position, _hitRadius, _hitResults, _damageConfig.TargetingLayer);
            var damaged = new HashSet<IResourceManageable>();

            for (var i = 0; i < hitCount; i++)
            {
                var target = GetResourceManageableFromHierarchy(_hitResults[i].gameObject);
                if (target != null && damaged.Add(target))
                    target.Deplete(_damageConfig.Amount);
            }

            Destroy(gameObject);
        }

        public void Initialize(Vector3 targetPosition)
        {
            _direction = (targetPosition - transform.position).normalized;
            transform.forward = _direction;
            
            _rigidbody.linearVelocity = _direction * _damageConfig.MoveSpeed;
            Invoke(nameof(DestroySelf), _damageConfig.LifeTime);
        }

        private void DestroySelf()
        {
            if (!_hasExploded)
                Destroy(gameObject);
        }

        private static IResourceManageable GetResourceManageableFromHierarchy(GameObject obj) =>
            obj.GetComponentInParent<IResourceManageable>()
            ?? obj.GetComponentInChildren<IResourceManageable>();
    }
}