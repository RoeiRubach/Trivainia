using UnityEngine;

namespace Trivainia
{
    public class PlayerAnimationsLocator : MonoBehaviour
    {
        [field: SerializeField] public AnimationClipSO Idle { get; private set; }
        [field: SerializeField] public AnimationClipSO Moving { get; private set; }
    }
}