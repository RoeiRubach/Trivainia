using UnityEngine;

namespace Trivainia
{
    public class AbilitySystem : MonoBehaviour
    {
        [SerializeField] private AbilityView _abilityView;
        [SerializeField] private AbilityData[] _startingAbilities;
        private AbilityController _controller;

        private void Start()
        {
            _abilityView.SetupButtons(GetComponent<PlayerServiceLocator>().Input);
            _controller = new AbilityController.Builder()
                          .WithAbilities(_startingAbilities)
                          .Build(_abilityView);
        }

        private void Update() => _controller.Update();
    }
}