using System;

namespace Trivainia
{
    public class HealthService : IHealthService
    {
        public event Action HealthChanged
        {
            add => _model.HealthChanged += value;
            remove => _model.HealthChanged -= value;
        }
        
        public event Action Depleted
        {
            add => _model.Depleted += value;
            remove => _model.Depleted -= value;
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