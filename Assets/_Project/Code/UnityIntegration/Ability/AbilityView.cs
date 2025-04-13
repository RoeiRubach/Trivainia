using System;
using System.Collections.Generic;
using UnityEngine;

namespace Trivainia
{
    public class AbilityView : MonoBehaviour
    {
        public AbilityButton[] Buttons;
        private AbilityView _abilityViewImplementation;

        public void SetupButtons(IInputReader input)
        {
            for (var i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].Initialize(i, input);
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