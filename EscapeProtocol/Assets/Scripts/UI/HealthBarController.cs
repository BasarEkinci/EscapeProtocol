using Controllers;
using Controllers.Enemy;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBarController : MonoBehaviour
    {
        [SerializeField] private HealthController healthController;
        
        [SerializeField] private Slider healthBar;
        [SerializeField] private Slider easeHealthBar;

        private float _lerpSpeed = 0.05f;

        private void Start()
        {
            healthBar.maxValue = healthController.MaxHealth;
            easeHealthBar.maxValue = healthController.MaxHealth;
        }
        
        private void Update()
        {
            healthBar.value = healthController.Health;
            easeHealthBar.value = Mathf.Lerp(easeHealthBar.value, healthController.Health, _lerpSpeed);
        }
    }
}
