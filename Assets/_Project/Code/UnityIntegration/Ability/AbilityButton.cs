using System;
using Sirenix.OdinInspector;
using Trivainia.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace Trivainia
{
    public class AbilityButton : MonoBehaviour
    {
        [SerializeField, ReadOnly] private int _index;
        [SerializeField, Required] private Image _radialImage;
        [SerializeField, Required] private Image _abilityIcon;
        [SerializeField, Required] private Image _abilityInput;

        public event Action<int> OnButtonPressed = delegate { };

        private void OnEnable() => GetComponent<Button>().onClick.AddListener(OnButtonClicked);
        private void OnDisable() => GetComponent<Button>().onClick.RemoveAllListeners();

        private void OnButtonClicked()
        {
            ConsoleLogger.Print($"Ability index {_index} been pressed");
            OnButtonPressed?.Invoke(_index);
        }

        public void RegisterListener(Action<int> listener) => OnButtonPressed += listener;

        public void Initialize(int index, IInputReader inputActionRef)
        {
            _index = index;
            inputActionRef.RegisterSkillAction(index, OnButtonClicked);
        }

        public void UpdateButtonSprite(Sprite newIcon) => _abilityIcon.sprite = newIcon;

        public void UpdateRadialFill(float progress)
        {
            if (_radialImage)
                _radialImage.fillAmount = progress;
        }

        public void UpdateButtonInput(Sprite sprite)
        {
            if (_abilityInput)
                _abilityInput.sprite = sprite;
        }
    }
}