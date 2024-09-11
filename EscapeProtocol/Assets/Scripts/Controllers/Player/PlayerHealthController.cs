using System;
using Combat;
using Data.UnityObjects;
using DG.Tweening;
using Objects;
using Signals;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerHealthController : MonoBehaviour, IDamageable
    {
        #region Serialized Fields
        [SerializeField] private GameObject explosionParticle;
        [SerializeField] private PlayerDataScriptable playerData;
        [SerializeField] private Material material;
        #endregion
        
        #region Public Fields
        public int Health => _currentHealth;
        public bool IsDead => _currentHealth <= 0;
        #endregion

        #region Private Variables
        private int _currentHealth;
        private Color _defaultColor;
        private Color _damagedColor;
        private Color _healColor;
        private AudioSource _audioSource;
        #endregion
        
        private void Awake()
        {
            _currentHealth = playerData.HealthData.Health;
        }

        private void Start()
        {
            _defaultColor = material.color;
            _damagedColor = Color.red;
            _healColor = Color.green;
        }

        private void OnEnable()
        {
            material.color = Color.white;
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            _currentHealth = Mathf.Max(_currentHealth, 0);
            material.DOColor(_damagedColor, 0.1f).OnComplete(() => material.DOColor(_defaultColor, 0.1f));
            if (_currentHealth <= 0)
            {
                Death();
            }
        }

        public void IncreaseHealth(int health)
        {
            _currentHealth += health;
            material.DOColor(_healColor, 0.1f).OnComplete(() => material.DOColor(_defaultColor, 0.1f));
            _currentHealth = Mathf.Min(_currentHealth, playerData.HealthData.Health);
        }
        private void Death()
        {
            PlayerSignals.Instance.OnPlayerDeath?.Invoke();
            CinemachineShake.Instance.ShakeCamera(3,0.22f);
            Instantiate(explosionParticle,transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
