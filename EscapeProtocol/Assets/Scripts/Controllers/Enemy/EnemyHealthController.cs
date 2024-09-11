using Combat;
using Data.UnityObjects;
using DG.Tweening;
using Objects;
using UnityEngine;

namespace Controllers.Enemy
{
    public class EnemyHealthController : MonoBehaviour, IDamageable
    {
        [SerializeField] private GameObject explosionParticle;
        [SerializeField] private GameObject enemyArea;
        [SerializeField] private GameObject thisGameObject;
        [SerializeField] private EnemyDataScriptable enemyData;
        [SerializeField] private Material material;
        [SerializeField] private Transform bodyTransform;
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
                Death();
            }
        }
        private void Death()
        {
            CinemachineShake.Instance.ShakeCamera(3,0.22f);
            Instantiate(explosionParticle,bodyTransform.position, Quaternion.identity);
            Destroy(enemyArea);
            Destroy(thisGameObject);
        }
    }
}