using Sirenix.OdinInspector;
using UnityEngine;

namespace Trivainia
{
    public abstract class AbilityExecutionStrategySO : ScriptableObject
    {
        [SerializeField, Required] protected Transform Origin;
        public abstract void Execute();
    }
}