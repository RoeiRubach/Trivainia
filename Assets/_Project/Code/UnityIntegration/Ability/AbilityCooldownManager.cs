using System;
using Cysharp.Threading.Tasks;
using ImprovedTimers;

namespace Trivainia
{
    public class AbilityCooldownManager
    {
        private readonly CountdownTimer _timer = new(0);

        public bool IsRunning => _timer.IsRunning;

        /// <summary>
        /// Raised every frame with the cooldown progress [0-1].
        /// </summary>
        public event Action<float> ProgressChanged;

        /// <summary>
        /// Raised when cooldown finishes.
        /// </summary>
        public event Action CooldownEnded;

        public bool CanQueue(float queueThreshold) => !_timer.IsRunning || _timer.Progress <= queueThreshold;

        public async UniTask RunCooldown(float duration)
        {
            _timer.Reset(duration);
            _timer.Start();

            while (_timer.IsRunning)
            {
                _timer.Tick();
                ProgressChanged?.Invoke(_timer.Progress);
                await UniTask.Yield();
            }

            ProgressChanged?.Invoke(0f);
            CooldownEnded?.Invoke();
        }
    }
}