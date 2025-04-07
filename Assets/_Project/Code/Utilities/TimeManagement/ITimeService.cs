using UnityEngine;

namespace Trivainia.Utilities
{
    public interface ITimeService
    {
        public float GetTime();
        public float GetDeltaTime();
        public float GetFixedDeltaTime();
    }

    public class UnityTime : ITimeService
    {
        public float GetTime() => Time.time;

        public float GetDeltaTime() => Time.deltaTime;

        public float GetFixedDeltaTime() => Time.fixedDeltaTime;
    }
}