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
        public bool IsPlayerDetected => _isPlayerDetected;
        public GameObject Target => _target;
        
        private bool _isPlayerDetected;
        private GameObject _target;
        private CancellationTokenSource _cancellationTokenSource;
        private bool _isGuarded = true;
        
        private string _enemyTag = "Player";
        
        private void OnTriggerEnter(Collider other)
        {
            CheckGuards();
            if (other.CompareTag(_enemyTag) && _isGuarded)
            {
                _target = other.gameObject;
                _cancellationTokenSource?.Cancel();
                _cancellationTokenSource = new CancellationTokenSource();
                HandleDetectionAfterDelay(_cancellationTokenSource.Token).Forget();
            }            
        }
        private void OnTriggerStay(Collider other)
        {
            if(_target != null)
            {
                if (_target.GetComponent<HealthController>().Health <= 0)
                {
                    _isPlayerDetected = false;
                    _target = null;
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag(_enemyTag))
            {
                _isPlayerDetected = false;
            }
        }
        private async UniTaskVoid HandleDetectionAfterDelay(CancellationToken token)
        {
            SoundManager.PLaySound(soundData, "Target", null, 1);
            await UniTask.Delay(1000, cancellationToken: token);
            if (!token.IsCancellationRequested)
            {
                _isPlayerDetected = true;
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
