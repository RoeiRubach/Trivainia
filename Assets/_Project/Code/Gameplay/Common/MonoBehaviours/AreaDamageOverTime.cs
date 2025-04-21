using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Trivainia
{
    public class AreaDamageOverTime : MonoBehaviour
    {
        [BoxGroup("Config"), MinValue(0f)]
        [SerializeField] private float _initialDelay = 1f;

        [BoxGroup("Config"), MinValue(0.1f)]
        [SerializeField] private float _tickInterval = 1f;

        [BoxGroup("Config"), Required]
        [SerializeField] private DamageConfigSO _damageConfig;

        private readonly Dictionary<IResourceManageable, Coroutine> _activeDamageCoroutines = new();

        private void OnTriggerEnter(Collider other)
        {
            var target = GetResourceManageableFromHierarchy(other.gameObject);
            if (target == null || _activeDamageCoroutines.ContainsKey(target))
                return;

            var coroutine = StartCoroutine(ApplyDamageOverTimeCoroutine(target));
            _activeDamageCoroutines[target] = coroutine;
        }

        private void OnTriggerExit(Collider other)
        {
            var target = GetResourceManageableFromHierarchy(other.gameObject);
            if (target == null)
                return;

            if (!_activeDamageCoroutines.TryGetValue(target, out var coroutine))
                return;
            StopCoroutine(coroutine);
            _activeDamageCoroutines.Remove(target);
        }

        private IEnumerator ApplyDamageOverTimeCoroutine(IResourceManageable target)
        {
            if (_initialDelay > 0f)
                yield return WaitFor.Seconds(_initialDelay);

            while (target != null)
            {
                ApplyDamageTick(target);
                yield return WaitFor.Seconds(_tickInterval);
            }
            _activeDamageCoroutines.Remove(target);
        }
        
        private static IResourceManageable GetResourceManageableFromHierarchy(GameObject obj)
        {
            return obj.GetComponentInParent<IResourceManageable>() 
                   ?? obj.GetComponentInChildren<IResourceManageable>();
        }

        private void ApplyDamageTick(IResourceManageable target) => target?.Deplete(_damageConfig.Amount);

        private void OnDisable()
        {
            foreach (var coroutine in _activeDamageCoroutines.Values.Where(coroutine => coroutine != null))
            {
                StopCoroutine(coroutine);
            }

            _activeDamageCoroutines.Clear();
        }
    }
}