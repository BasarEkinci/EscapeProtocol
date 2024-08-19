using System.Threading;
using Cysharp.Threading.Tasks;
using Data.UnityObjects;
using Managers;
using UnityEngine;

namespace Combat
{
    public class EnemyArea : MonoBehaviour
    {
        [SerializeField] private SoundDataScriptable soundData;
        public bool IsEnemyDetected => _isEnemyDetected;
        public GameObject Enemy => _enemy;
    
        private bool _isEnemyDetected;
        private GameObject _enemy;
        private CancellationTokenSource _cancellationTokenSource;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _cancellationTokenSource?.Cancel();
                _cancellationTokenSource = new CancellationTokenSource();
                HandleDetectionAfterDelay(_cancellationTokenSource.Token).Forget();
                _enemy = other.gameObject;
            }            
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _cancellationTokenSource?.Cancel();
                _isEnemyDetected = false;
                _enemy = null;
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
    }
}
