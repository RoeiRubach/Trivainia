using Trivainia.Utilities;
using UnityEngine;

namespace Trivainia
{
    [CreateAssetMenu(fileName = "DashSpellStrategy", menuName = "Scriptable Objects/Spells/DashSpellStrategy")]
    public class DashAbilityStrategy : AbilityExecutionStrategySO
    {
        [SerializeField] private float _dashSpeed = 10f;
        private ILocomotionService _locomotion;
        
        public override void Execute()
        {
            if (Origin == null)
            {
                ConsoleLogger.PrintWarning("DashAbility: Origin is not assigned.");
                return;
            }
            
            _locomotion ??= FindAnyObjectByType<PlayerServiceLocator>().MovementService;
            
            _locomotion.ApplyExternalVelocity(_dashSpeed * Origin.forward);
        }
    }
}