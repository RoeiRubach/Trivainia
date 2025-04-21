using System;
using UnityEngine;

namespace Trivainia
{
    public class SearchRadiusGizmo : MonoBehaviour
    {
        [SerializeField] private float _radius = 15f;
        [SerializeField] private Color _color = new Color(1f, 0f, 0f, 0.25f);
        [SerializeField] private FireballAbilityStrategy _fireballStrategy;

        private void OnValidate() => _fireballStrategy.SearchRadius = _radius;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = _color;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}