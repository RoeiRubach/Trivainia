using System;
using System.Collections.Generic;
using Sirenix.Serialization;
using UnityEngine;

namespace Trivainia
{
    public class AbilityView : MonoBehaviour
    {
        public AbilityButton[] Buttons;
        [OdinSerialize] private IInputReader _input;
        private AbilityView _abilityViewImplementation;

        private void Awake()
        {
            for (var i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].Initialize(i, _input);
                UpdateRadial(0);
            }
        }

        public void UpdateRadial(float progress)
        {
            if (float.IsNaN(progress))
                progress = 0;
            Array.ForEach(Buttons, button => button.UpdateRadialFill(progress));
        }

        public void UpdateButtonSprites(List<Ability> abilities)
        {
            for (var i = 0; i < Buttons.Length; i++)
                if (i < abilities.Count)
                    Buttons[i].UpdateButtonSprite(abilities[i].Data.Icon);
        }
    }
}