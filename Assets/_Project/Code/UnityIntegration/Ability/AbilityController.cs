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
        }

        private void OnAbilityButtonPressed(int index)
        {
            EventSystem.current.SetSelectedGameObject(null);

            if (!_model.TryGetAbility(index, out var ability))
                return;

            if (_cooldown.CanQueue(ability.Data.QueueThreshold))
                _queue.Enqueue(ability.CreateCommand());

            if (!_cooldown.IsRunning)
                ExecuteNextAsync().Forget();
        }

        private async UniTaskVoid ExecuteNextAsync()
        {
            if (!_queue.TryDequeue(out var cmd))
                return;

            cmd.Execute();
            await _cooldown.RunCooldown(cmd.Duration);
            ExecuteNextAsync().Forget();
        }

        public class Builder
        {
            private readonly AbilityModel _model = new();

            public Builder WithAbilities(AbilityData[] abilitiesData)
            {
                foreach (var data in abilitiesData)
                    _model.Add(new Ability(data));

                return this;
            }

            public AbilityController Build(AbilityView view) => new(view, _model);
        }
    }
}