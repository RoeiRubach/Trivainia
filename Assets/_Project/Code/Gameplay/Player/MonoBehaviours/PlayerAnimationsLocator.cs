using Sirenix.OdinInspector;
using UnityEngine;

namespace Trivainia
{
    public class PlayerAnimationsLocator : MonoBehaviour
    {
        [field: SerializeField, Required] public AnimationClipSO Idle { get; private set; }
        [field: SerializeField, Required] public AnimationClipSO Moving { get; private set; }
    }
}