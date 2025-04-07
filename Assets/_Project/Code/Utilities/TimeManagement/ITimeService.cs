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
        public float GetTime() => UnityEngine.Time.time;

        public float GetDeltaTime() => UnityEngine.Time.deltaTime;

        public float GetFixedDeltaTime() => UnityEngine.Time.fixedDeltaTime;
    }

}