using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using ImprovedTimers;

namespace Trivainia
{
    public class AbilityCooldownManager
    {
        private readonly Dictionary<int, CountdownTimer> _cooldowns = new();

        public bool IsRunning(int index)
            => _cooldowns.ContainsKey(index) && _cooldowns[index].IsRunning;

        public bool CanQueue(int index, float queueThreshold)
        {
            if (!_cooldowns.TryGetValue(index, out var timer))
                return true;

            return !timer.IsRunning || timer.Progress <= queueThreshold;
        }

        public event Action<int, float> ProgressChanged;
        public event Action<int> CooldownEnded;

        public async UniTask RunCooldown(int index, float duration)
        {
            var timer = new CountdownTimer(duration);
            _cooldowns[index] = timer;

            timer.Start();
            while (timer.IsRunning)
            {
                timer.Tick();
                ProgressChanged?.Invoke(index, timer.Progress);
                await UniTask.Yield();
            }

            ProgressChanged?.Invoke(index, 0f);
            CooldownEnded?.Invoke(index);
            _cooldowns.Remove(index);
        }
    }
}