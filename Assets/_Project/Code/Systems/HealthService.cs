using System;

namespace Trivainia
{
    public class HealthService : IHealthService
    {
        public event Action OnHealthChanged
        {
            add => _model.OnHealthChanged += value;
            remove => _model.OnHealthChanged -= value;
        }

        private readonly HealthModel _model;
        
        public float Max => _model.Max;
        public float Ratio => _model.Ratio;
        public bool IsFull => _model.IsFull;
        public float Current => _model.Current;
        public bool IsDepleted => _model.IsDepleted;

        public HealthService(HealthConfigSO config) => _model = new HealthModel(config.InitialValue, config.MaxValue);

        public void Reset() => _model.Reset();
        public void Restore(float amount) => _model.Restore(amount);
        public void Deplete(float amount) => _model.Deplete(amount);
    }
}