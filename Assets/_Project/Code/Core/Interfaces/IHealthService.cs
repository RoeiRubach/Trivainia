namespace Trivainia
{
    public interface IHealthService : IResourceManageable
    {
        public event System.Action Depleted;
        public event System.Action HealthChanged;
        public float Current { get; }
        public float Max { get; }
        public float Ratio { get; }
        public bool IsDepleted { get; }
        public bool IsFull { get; }
    }
}