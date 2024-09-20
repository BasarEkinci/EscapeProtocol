using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Data.UnityObjects;
using Inputs;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers.Player
{
    public class PlayerGunController : MonoBehaviour, IGunController
    {
        #region Serilized Fields

        [SerializeField] private GunDataScriptable gunData;
        [SerializeField] private SoundDataScriptable soundData;
        [SerializeField] private Transform firePoint;
        [SerializeField] private GameObject bulletPrefab;
        
        #endregion
        
        public float FireRate { get; set; }

        #region Private Variables
        
        private CancellationTokenSource _cancellationTokenSource;
        private InputHandler _inputHandler;
        private bool _canBaseShoot = true;
        private bool _isFiring;

        #region Object Pooling Variables
        private Queue<GameObject> _bullets;
        private const int BulletPoolSize = 20;
        private const int BulletSpeed = 50;
        private const int BulletReturnTime = 350;
        #endregion

        #endregion
        

        private void Awake()
        {
            _inputHandler = new InputHandler();
            _bullets = new Queue<GameObject>();
            for (int i = 0; i < BulletPoolSize; i++)
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
            if (GameManager.Instance.IsGamePaused)
            {
                return;
            }
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

        /// <summary>
        /// To fire the gun repeatedly. If the player is holding the attack button then the gun will fire repeatedly. Otherwise, it will be firing at once.
        /// </summary>
        /// <param name="token"></param>
        public async UniTaskVoid FireRepeatedly(CancellationToken token)
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
            _isFiring = false;
        }



        public void Fire()
        {       
            if (_bullets.Count > 0)
            {
                GameObject bullet = _bullets.Dequeue();
                bullet.SetActive(true);
                bullet.transform.position = firePoint.position;
                bullet.transform.rotation = firePoint.rotation;
                bullet.GetComponent<Rigidbody>().velocity = firePoint.transform.forward * -BulletSpeed;                
                ReturnToPool(bullet).Forget();
            }
            SoundManager.PLaySound(soundData,"LaserGun");
        }

        private async UniTaskVoid ReturnToPool(GameObject bullet)
        {
            await UniTask.Delay(BulletReturnTime);
            if(bullet.activeSelf)
            {
                bullet.SetActive(false);
                bullet.transform.position = firePoint.position;
            }
            _bullets.Enqueue(bullet);
        }
    }
}
