using Sirenix.Serialization;
using UnityEngine;

namespace Trivainia
{
    public class AbilitySystem : MonoBehaviour
    {
        [OdinSerialize] private AbilityView _abilityView;
        [SerializeField] private AbilityData[] _startingAbilities;
        private AbilityController _controller;

        private void Awake() =>
            _controller = new AbilityController.Builder()
                          .WithAbilities(_startingAbilities)
                          .Build(_abilityView);

        private void Update() => _controller.Update();
    }
}