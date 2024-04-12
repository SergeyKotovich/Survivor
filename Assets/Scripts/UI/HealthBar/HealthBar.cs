using System;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using Slider = UnityEngine.UI.Slider;

public class HealthBar : MonoBehaviour
{
   [SerializeField] private Slider _healthBar;
   [SerializeField] private Gradient _gradient;
   [SerializeField] private Image _fill;
   private IHealth _healthController;

   [Inject]
   public void Construct(PlayerController playerController)
   {
      _healthController = playerController.HealthController;
      _healthController.HealthChanged += UpdatePlayerHealth;
   }

   private void UpdatePlayerHealth()
   {
      _healthBar.value = _healthController.Health / _healthController.MaxHealth;
      var color = _gradient.Evaluate(_healthBar.value);
      _fill.color = color;
   }

   private void OnDestroy()
   {
      _healthController.HealthChanged -= UpdatePlayerHealth;
   }
}