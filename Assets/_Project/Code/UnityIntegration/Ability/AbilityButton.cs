using System;
using UnityEngine;
using UnityEngine.UI;

namespace Trivainia
{
    public class AbilityButton : MonoBehaviour
    {
        [SerializeField] private int _index;
        [SerializeField] private Image _radialImage;
        [SerializeField] private Image _abilityIcon;

        public event Action<int> OnButtonPressed = delegate { };

        private void OnEnable() => GetComponent<Button>().onClick.AddListener(OnButtonClicked);
        private void OnDisable() => GetComponent<Button>().onClick.RemoveAllListeners();

        private void OnButtonClicked() => OnButtonPressed?.Invoke(_index);

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
    }
}