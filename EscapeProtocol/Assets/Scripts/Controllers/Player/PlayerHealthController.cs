using System;
using Combat;
using Cysharp.Threading.Tasks;
using Data.UnityObjects;
using DG.Tweening;
using Managers;
using Objects;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerHealthController : MonoBehaviour, IDamageable
    {
        #region Serialized Fields
        [SerializeField] private ParticleSystem explosionParticle;
        [SerializeField] private SoundDataScriptable soundData;
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

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            _currentHealth = Mathf.Max(_currentHealth, 0);
            material.DOColor(_damagedColor, 0.1f).OnComplete(() => material.DOColor(_defaultColor, 0.1f));
            if (_currentHealth <= 0)
            {
                Death().Forget();
            }
        }

        public void IncreaseHealth(int health)
        {
            _currentHealth += health;
            material.DOColor(_healColor, 0.1f).OnComplete(() => material.DOColor(_defaultColor, 0.1f));
            _currentHealth = Mathf.Min(_currentHealth, playerData.HealthData.Health);
        }
        private async UniTaskVoid Death()
        {
            SoundManager.PLaySound(soundData, "Explosion");
            CinemachineShake.Instance.ShakeCamera(3,0.22f);
            explosionParticle.Play();
            await UniTask.Delay(TimeSpan.FromSeconds(0.5), ignoreTimeScale: false);
            gameObject.SetActive(false);
        }
    }
}
