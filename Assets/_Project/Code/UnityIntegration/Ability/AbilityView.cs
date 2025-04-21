using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Trivainia
{
    public class AbilityView : MonoBehaviour
    {
        [field: SerializeField, ValidateInput(nameof(HasAtLeastOneButton), "At least one AbilityButton is required.")]
        public AbilityButton[] Buttons { get; private set; }

        private AbilityView _abilityViewImplementation;
        [SerializeField, Required] private PlayerInputSpritesLocator _inputSpritesLocator;

        private bool HasAtLeastOneButton() => Buttons is {Length: > 0};
        
        public void SetupView(IInputReader input)
        {
            input.InputDeviceChanged += UpdateButtonInputs;
            for (var i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].Initialize(i, input);
                UpdateRadial(i, 0);
            }
        }

        public void UpdateRadial(int index, float progress)
        {
            if (float.IsNaN(progress))
                progress = 0;

            if (index >= 0 && index < Buttons.Length)
                Buttons[index].UpdateRadialFill(progress);
        }

        public void UpdateButtonSprites(List<Ability> abilities)
        {
            for (var i = 0; i < Buttons.Length; i++)
                if (i < abilities.Count)
                    Buttons[i].UpdateButtonSprite(abilities[i].DataSo.Icon);
                else
                    Buttons[i].gameObject.SetActive(false);
        }

        public void UpdateButtonInputs(InputDeviceType inputDeviceType)
        {
            var sprites = _inputSpritesLocator.GetSpritesClockwiseFromBottom(inputDeviceType).ToArray();

            for (var i = 0; i < Buttons.Length; i++)
            {
                if (i < sprites.Length)
                    Buttons[i].UpdateButtonInput(sprites[i]);
            }
        }

    }
}