using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.EventSystems;

namespace Trivainia
{
    public class AbilityController
    {
        private readonly AbilityModel _model;
        private readonly AbilityView _view;
        private readonly AbilityQueue _queue;
        private readonly AbilityCooldownManager _cooldown;

        private AbilityController(AbilityView view, AbilityModel model)
        {
            _view = view;
            _model = model;
            _queue = new AbilityQueue();
            _cooldown = new AbilityCooldownManager();

            ConnectModel();
            ConnectView();
        }

        private void ConnectModel() => _model.AbilityAdded += _view.UpdateButtonSprites;

        private void ConnectView()
        {
            _cooldown.ProgressChanged += _view.UpdateRadial;

            foreach (var button in _view.Buttons)
                button.RegisterListener(OnAbilityButtonPressed);

            _view.UpdateButtonSprites(_model.Abilities);
            _view.UpdateButtonInputs(InputDeviceType.KeyboardMouse);
        }

        private void OnAbilityButtonPressed(int index)
        {
            EventSystem.current.SetSelectedGameObject(null);

            if (!_model.TryGetAbility(index, out var ability))
                return;

            if (_cooldown.CanQueue(index, ability.DataSo.CoyoteThreshold))
                _queue.Enqueue(index, ability.GetOrCreateCommand());

            if (!_cooldown.IsRunning(index))
                ExecuteNextAsync().Forget();
        }

        private async UniTaskVoid ExecuteNextAsync()
        {
            if (!_queue.TryDequeue(out var queued))
                return;

            queued.Command.Execute();
            await _cooldown.RunCooldown(queued.Index, queued.Command.CooldownDuration);
            ExecuteNextAsync().Forget();
        }

        public class Builder
        {
            private readonly AbilityModel _model = new();

            public Builder WithAbilities(AbilityDataSO[] abilitiesData, PlayerAnimatorController animator)
            {
                foreach (var data in abilitiesData)
                    _model.Add(new Ability(data, animator));

                return this;
            }

            public AbilityController Build(AbilityView view) => new(view, _model);
        }
    }
}