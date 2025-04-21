using System;

namespace Trivainia
{
    public class HealthService : IHealthService
    {
        private readonly HealthModel _model;

        public float Current => _model.Current;
        public float Max => _model.Max;
        public bool IsDepleted => _model.IsDepleted;
        public bool IsFull => _model.IsFull;

        public event Action<float> OnHealthChanged
        {
            add => _model.OnHealthChanged += value;
            remove => _model.OnHealthChanged -= value;
        }

        public HealthService(HealthConfigSO config) => _model = new HealthModel(config.InitialValue, config.MaxValue);

        public void Restore(float amount) => _model.Restore(amount);
        public void Deplete(float amount) => _model.Deplete(amount);
        public void Reset() => _model.Reset();
    }
}