using UnityEngine;

namespace Trivainia
{
    public class AbilitySystem : MonoBehaviour
    {
        [SerializeField] private AbilityView _abilityView;
        [SerializeField] private AbilityData[] _startingAbilities;

        private void Start()
        {
            _abilityView.SetupButtons(GetComponent<PlayerServiceLocator>().Input);
            InitializeController();
        }

        private void InitializeController() =>
            new AbilityController.Builder()
                .WithAbilities(_startingAbilities)
                .Build(_abilityView);
    }
}