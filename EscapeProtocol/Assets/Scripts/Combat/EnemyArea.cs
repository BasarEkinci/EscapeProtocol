using System.Threading;
using Controllers;
using Cysharp.Threading.Tasks;
using Data.UnityObjects;
using Managers;
using UnityEngine;

namespace Combat
{
    public class EnemyArea : MonoBehaviour
    {
        [SerializeField] private SoundDataScriptable soundData;
        [SerializeField] private GameObject currentGuard;
        public bool IsEnemyDetected => _isEnemyDetected;
        public bool IsEnemyKilled => _isEnemyKilled;
        public GameObject Enemy => _enemy;
        private bool _isEnemyKilled;
        private bool _isEnemyDetected;
        private GameObject _enemy;
        private CancellationTokenSource _cancellationTokenSource;
        private bool _isGuarded = true;
        
        private string _enemyTag = "Player";
        
        private void OnTriggerEnter(Collider other)
        {
            CheckGuards();
            if (other.CompareTag(_enemyTag) && _isGuarded)
            {
                _enemy = other.gameObject;
                _cancellationTokenSource?.Cancel();
                _isEnemyKilled = false;
                _cancellationTokenSource = new CancellationTokenSource();
                HandleDetectionAfterDelay(_cancellationTokenSource.Token).Forget();
            }            
        }

        private void OnTriggerStay(Collider other)
        {
            if(_enemy != null)
            {
                if (_enemy.GetComponent<HealthController>().Health <= 0)
                {
                    _isEnemyDetected = false;
                    _enemy = null;
                    _isEnemyKilled = true;
                    Debug.Log(_isEnemyKilled);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag(_enemyTag))
            {
                _isEnemyDetected = false;
            }
        }

        private async UniTaskVoid HandleDetectionAfterDelay(CancellationToken token)
        {
            SoundManager.PLaySound(soundData, "Target", null, 1);
            await UniTask.Delay(1000, cancellationToken: token);
            if (!token.IsCancellationRequested)
            {
                _isEnemyDetected = true;
            }
        }

        private void CheckGuards()
        {
            if(!currentGuard.activeSelf)
            {
                _isGuarded = false;
            }
        }
    }
}
