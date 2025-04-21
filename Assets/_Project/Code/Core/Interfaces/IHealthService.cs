namespace Trivainia
{
    public interface IHealthService : IResourceManageable
    {
        public event System.Action OnHealthChanged;
        public float Current { get; }
        public float Max { get; }
        public float Ratio { get; }
        public bool IsDepleted { get; }
        public bool IsFull { get; }
    }
}