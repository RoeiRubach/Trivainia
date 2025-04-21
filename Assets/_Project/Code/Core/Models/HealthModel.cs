using UnityEngine;

namespace Trivainia
{
    public class HealthModel
    {
        public event System.Action<float> OnHealthChanged;

        public float Current { get; private set; }
        public float Max { get; }

        public bool IsDepleted => Current <= 0;
        public bool IsFull => Current >= Max;
        public float Ratio => Current / Max;

        public HealthModel(float initial, float max)
        {
            Max = max;
            Current = Mathf.Clamp(initial, 0, max);
        }

        public void Restore(float amount)
        {
            if (amount <= 0 || IsFull) return;
            Current = Mathf.Min(Current + amount, Max);
            OnHealthChanged?.Invoke(Current);
        }

        public void Deplete(float amount)
        {
            if (amount <= 0 || IsDepleted) return;
            Current = Mathf.Max(Current - amount, 0);
            OnHealthChanged?.Invoke(Current);
        }

        public void Reset()
        {
            Current = Max;
            OnHealthChanged?.Invoke(Current);
        }
    }
}