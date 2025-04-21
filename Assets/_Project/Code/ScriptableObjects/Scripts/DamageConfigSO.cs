using UnityEngine;

namespace Trivainia
{
    [CreateAssetMenu(fileName = "DamageConfigSO", menuName = "Scriptable Objects/DamageConfigSO")]
    public class DamageConfigSO : ScriptableObject
    {
        [field: SerializeField, Min(0)] public float Amount { get; private set; }
    }
}