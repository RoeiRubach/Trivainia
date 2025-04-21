namespace Trivainia
{
    public interface IHealthService
    {
        public event System.Action OnHealthChanged;
        public float Current { get; }
        public float Max { get; }
        public float Ratio { get; }
        public bool IsDepleted { get; }
        public bool IsFull { get; }
        public void Deplete(float amount);
        public void Restore(float amount);
        public void Reset();
    }
}