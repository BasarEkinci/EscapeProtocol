using System;
using Combat;
using Cysharp.Threading.Tasks;
using Data.UnityObjects;
using DG.Tweening;
using Managers;
using UnityEngine;

namespace Controllers.Enemy
{
    public class EnemyHealthController : MonoBehaviour, IDamageable
    {
        [SerializeField] private ParticleSystem explosionParticle;
        [SerializeField] private SoundDataScriptable soundData;
        [SerializeField] private EnemyDataScriptable enemyData;
        [SerializeField] private Material material;
        public int Health => _currentHealth;
        public bool IsDead => _currentHealth <= 0;
        
        
        private int _currentHealth;
        private Color _defaultColor;
        private Color _damagedColor;
        
        private void Awake()
        {
            _currentHealth = enemyData.HealthData.Health;
        }

        private void Start()
        {
            _defaultColor = material.color;
            _damagedColor = Color.red;
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            material.DOColor(_damagedColor, 0.1f).OnComplete(() => material.DOColor(_defaultColor, 0.1f));
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