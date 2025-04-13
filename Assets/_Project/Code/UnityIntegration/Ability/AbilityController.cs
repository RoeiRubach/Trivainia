using System.Collections.Generic;
using ImprovedTimers;
using UnityEngine.EventSystems;

namespace Trivainia
{
    public class AbilityController
    {
        private readonly AbilityView _view;
        private readonly AbilityModel _model;
        private readonly CountdownTimer _timer = new(0);
        private readonly Queue<AbilityCommand> _abilityQueue = new();

        private AbilityController(AbilityView view, AbilityModel model)
        {
            _view = view;
            _model = model;

            ConnectModel();
            ConnectView();
        }

        private void ConnectModel() => _model.AbilityAdded += UpdateButtons;

        private void ConnectView()
        {
            foreach (var button in _view.Buttons)
                button.RegisterListener(OnAbilityButtonPressed);

            _view.UpdateButtonSprites(_model.Abilities);
        }

        public void Update()
        {
            _timer.Tick();
            _view.UpdateRadial(_timer.Progress);

            if (!_timer.IsRunning && _abilityQueue.TryDequeue(out var cmd))
            {
                cmd.Execute();
                _timer.Reset(cmd.Duration);
                _timer.Start();
            }
        }

        private void UpdateButtons(List<Ability> updatedAbilities) => _view.UpdateButtonSprites(updatedAbilities);

        private void OnAbilityButtonPressed(int index)
        {
            if (_timer.Progress < 0.25f || !_timer.IsRunning)
                if (_model.Abilities[index] != null)
                    _abilityQueue.Enqueue(_model.Abilities[index].CreateCommand());
            EventSystem.current.SetSelectedGameObject(null);
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