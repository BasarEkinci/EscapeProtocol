using System;
using Combat;
using Cysharp.Threading.Tasks;
using Data.UnityObjects;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class HealthController : MonoBehaviour,IDamageable
    {
        [SerializeField] private ParticleSystem explosionParticle;
        [SerializeField] private float maxHealth = 100;
        [SerializeField] private SoundDataScriptable soundData;
        public float CurrentHealth => _currentHealth;
        public float MaxHealth => maxHealth;
        public bool IsDead => _currentHealth <= 0;
        
        private float _currentHealth;
        
        
        private void Start()
        {
            _currentHealth = maxHealth;
            Debug.Log(gameObject.name + " : " + _currentHealth);
        }
        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            _currentHealth = Mathf.Max(_currentHealth, 0);
            if (_currentHealth <= 0)
            {
                Death().Forget();
            }
        }
        private async UniTaskVoid Death()
        {
            SoundManager.PLaySound(soundData, "Explosion",null,1);
            explosionParticle.Play();
            await UniTask.Delay(TimeSpan.FromSeconds(0.5), ignoreTimeScale: false);
            gameObject.SetActive(false);
        }
    }
}
