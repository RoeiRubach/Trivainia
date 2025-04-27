using UnityEngine;

namespace Trivainia
{
    [CreateAssetMenu(fileName = "HealingAbilityStrategy", menuName = "Scriptable Objects/Abilities/HealingAbilityStrategy")]
    public class HealingAbilityStrategy : AbilityExecutionStrategySO
    {
        [SerializeField, Min(0)] private float _amount = 20;
        private IResourceManageable _resourceManageable;

        public override void Execute()
        {
            if(Origin == null)
                Origin = FindAnyObjectByType<PlayerController>().transform;
            _resourceManageable ??= Origin.GetComponentInChildren<IResourceManageable>();
            _resourceManageable.Restore(_amount);
        }
    }
}