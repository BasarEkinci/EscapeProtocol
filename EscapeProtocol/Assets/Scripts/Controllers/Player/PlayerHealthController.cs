using System;
using Combat;
using Cysharp.Threading.Tasks;
using Data.UnityObjects;
using Managers;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerHealthController : MonoBehaviour, IDamageable
    {
        [SerializeField] private ParticleSystem explosionParticle;
        [SerializeField] private SoundDataScriptable soundData;
        [SerializeField] private PlayerDataScriptable playerData;
        public int Health => _currentHealth;
        public bool IsDead => _currentHealth <= 0;
       
        
        private int _currentHealth;
        private void Awake()
        {
            _currentHealth = playerData.HealthData.Health;
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
            SoundManager.PLaySound(soundData, "Explosion");
            explosionParticle.Play();
            await UniTask.Delay(TimeSpan.FromSeconds(0.5), ignoreTimeScale: false);
            gameObject.SetActive(false);
        }
    }
}
