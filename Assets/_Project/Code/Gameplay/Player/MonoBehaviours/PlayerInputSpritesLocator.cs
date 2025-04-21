using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Trivainia
{
    public class PlayerInputSpritesLocator : MonoBehaviour
    {
        [SerializeField, Required] private AbilitiesInputImagesSO _keyboardInputSprites;
        [SerializeField, Required] private AbilitiesInputImagesSO _controllerInputSprites;

        public IEnumerable<Sprite> GetSpritesClockwiseFromBottom(InputDeviceType deviceType) =>
            deviceType switch {
                InputDeviceType.KeyboardMouse => _keyboardInputSprites.GetImagesClockwiseFromBottom(),
                InputDeviceType.Gamepad       => _controllerInputSprites.GetImagesClockwiseFromBottom(),
                _                             => throw new System.ArgumentOutOfRangeException(nameof(deviceType), deviceType, null)
            };
    }
}