using Sirenix.OdinInspector;
using UnityEngine;

namespace Trivainia
{
    public class AbilitySystem : MonoBehaviour
    {
        [SerializeField, Required] private AbilityView _abilityView;
        [SerializeField] private AbilityDataSO[] _startingAbilities;

        private void Start()
        {
            _abilityView.SetupView(GetComponent<PlayerServiceLocator>().Input);
            InitializeController();
        }

        private void InitializeController() =>
            new AbilityController.Builder()
                .WithAbilities(_startingAbilities, FindAnyObjectByType<PlayerAnimatorController>())
                .Build(_abilityView);
    }
}