using Combat;
using UnityEngine;

namespace Controllers
{
    public class HealthController : MonoBehaviour,IDamageable
    {
        [SerializeField] private float maxHealth = 100;

        private float _currentHealth;

        private void Start()
        {
            _currentHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            _currentHealth = Mathf.Max(_currentHealth, 0);
            Debug.Log(_currentHealth);
        }
    }
}
