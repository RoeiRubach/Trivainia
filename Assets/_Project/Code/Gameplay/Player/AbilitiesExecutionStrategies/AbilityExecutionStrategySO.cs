using UnityEngine;

namespace Trivainia
{
    public abstract class AbilityExecutionStrategySO : ScriptableObject
    {
        [SerializeField] protected Transform Origin;
        public abstract void Execute();
    }
}