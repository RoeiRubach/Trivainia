using UnityEngine;

namespace Trivainia
{
    public abstract class AbilityExecutionStrategySO : ScriptableObject
    {
        protected Transform Origin = null;
        public abstract void Execute();
    }
}