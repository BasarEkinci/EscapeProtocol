using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Data.UnityObjects;
using Inputs;
using Managers;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerGunController : MonoBehaviour
    {
        
        [SerializeField] private GunDataScriptable gunData;
        [SerializeField] private SoundDataScriptable soundData;
        [SerializeField] private Transform firePoint;
        [SerializeField] private GameObject bulletPrefab;
        
        private Queue<GameObject> _bullets;
        private CancellationTokenSource _cancellationTokenSource;
        private InputHandler _inputHandler;
        private bool _canBaseShoot = true;
        private bool _isFiring;

        private void Awake()
        {
            _inputHandler = new InputHandler();
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
            if (_inputHandler.GetAttackInput() && _canBaseShoot && !_isFiring)
            {
                _isFiring = true;
                FireRepeatedly(_cancellationTokenSource.Token).Forget();
            }
            else if(!_inputHandler.GetAttackInput())
            {
                _isFiring = false;
            }
        }

        private void OnDisable()
        {
            _cancellationTokenSource.Cancel();
        }


        private async UniTaskVoid FireRepeatedly(CancellationToken token)
        { 
            while (_inputHandler.GetAttackInput() && !token.IsCancellationRequested) 
            { 
                if (_canBaseShoot) 
                { 
                    Fire(); 
                    _canBaseShoot = false; 
                    await UniTask.Delay(TimeSpan.FromSeconds(gunData.GunData.BaseAttackFireRate), cancellationToken: token);
                    _canBaseShoot = true;
                }
                await UniTask.Yield();
            }
        }

        private void Fire()
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
            SoundManager.PLaySound(soundData,"LaserGun",null,1);
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
