using UnityEngine;

namespace Trivainia
{
    public abstract class AbilityExecutionStrategySO : ScriptableObject
    {
        public Transform Origin;
        public abstract void Execute();
    }
}