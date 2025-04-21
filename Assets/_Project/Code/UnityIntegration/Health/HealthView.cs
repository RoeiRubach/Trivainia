using UnityEngine;
using UnityEngine.UI;

namespace Trivainia
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private Image _fillImage;

        private IHealthService _health;

        public void Initialize(IHealthService health)
        {
            _health = health;
            _health.OnHealthChanged += OnHealthChanged;
            OnHealthChanged();
        }

        private void OnHealthChanged()
        {
            var fillAmount = Mathf.Clamp01(_health.Ratio);
            _fillImage.fillAmount = fillAmount;
        }

        private void OnDestroy()
        {
            if (_health != null)
                _health.OnHealthChanged -= OnHealthChanged;
        }
    }
}