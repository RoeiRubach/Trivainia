using Trivainia.Utilities;
using UnityEngine;

namespace Trivainia
{
    [CreateAssetMenu(fileName = "DashSpellStrategy", menuName = "Scriptable Objects/Abilities/DashSpellStrategy")]
    public class DashAbilityStrategy : AbilityExecutionStrategySO
    {
        private const int PHYSICS_MULTIPLIER = 100;
        [SerializeField, Min(0)] private float _dashForce = 2f;
        private ILocomotionService _locomotion;

        public override void Execute()
        {
            if (Origin == null)
            {
                ConsoleLogger.PrintWarning("DashAbility: Origin is not assigned.");

                return;
            }

            _locomotion ??= FindAnyObjectByType<PlayerServiceLocator>().MovementService;

            _locomotion.ApplyExternalVelocity(_dashForce * PHYSICS_MULTIPLIER * Origin.forward);
        }
    }
}