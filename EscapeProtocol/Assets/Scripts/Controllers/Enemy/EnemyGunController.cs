using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Data.UnityObjects;
using Managers;
using UnityEngine;

namespace Controllers.Enemy
{
    public class EnemyGunController : MonoBehaviour,IGunController
    {
        public float FireRate { get; set; }
        
        [Header("Data")]
        [SerializeField] private SoundDataScriptable soundData;
        
        [Header("Firing Settings")]
        [SerializeField] private Transform firePoint;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private int fireRate;
        
        private Queue<GameObject> _bullets;
        private CancellationTokenSource _cancellationTokenSource;
        private bool _canBaseShoot = true;
        private bool _isFiring;

        private void Awake()
        {
            _bullets = new Queue<GameObject>();

            for (int i = 0; i < 20; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab);
                bullet.SetActive(false); 
                _bullets.Enqueue(bullet);
            }
        }

        private void OnEnable()
        {
            _cancellationTokenSource = new CancellationTokenSource();
        }

        private void Update()
        {
            if (_canBaseShoot && !_isFiring)
            {
                FireRepeatedly(_cancellationTokenSource.Token).Forget();
            }
        }

        private void OnDisable()
        {
            _cancellationTokenSource.Cancel();
        }


        // ReSharper disable Unity.PerformanceAnalysis
        public async UniTaskVoid FireRepeatedly(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested) 
                { 
                    token.ThrowIfCancellationRequested();
                    if (_canBaseShoot) 
                    { 
                        Fire(); 
                        _canBaseShoot = false; 
                        await UniTask.Delay(TimeSpan.FromSeconds(fireRate), cancellationToken: token); 
                        _canBaseShoot = true; 
                    } 
                    await UniTask.Yield();
                }
            }
            finally
            {
                _isFiring = false;
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis


        public void Fire()
        {        
            if (_bullets.Count > 0)
            {
                GameObject bullet = _bullets.Dequeue();
                bullet.SetActive(true);
                bullet.transform.position = firePoint.position;
                bullet.transform.rotation = firePoint.rotation;
                bullet.GetComponent<Rigidbody>().velocity = firePoint.transform.forward * -50;                
                ReturnToPool(bullet).Forget();
            }
            if(soundData != null)
            {
                SoundManager.PLaySound(soundData, "LaserGun",null, 1f);
            }
        }

        private async UniTaskVoid ReturnToPool(GameObject bullet)
        {
            await UniTask.Delay(3500);
            if(bullet.activeSelf)
                bullet.SetActive(false);
            _bullets.Enqueue(bullet);
        }
    }
}