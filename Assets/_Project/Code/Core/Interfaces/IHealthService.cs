namespace Trivainia
{
    public interface IHealthService
    {
        float Current { get; }
        float Max { get; }
        bool IsDepleted { get; }
        bool IsFull { get; }

        void Deplete(float amount);
        void Restore(float amount);
        void Reset();
    }
}