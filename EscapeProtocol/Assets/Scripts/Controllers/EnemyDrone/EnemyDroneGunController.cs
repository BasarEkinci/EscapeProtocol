using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Data.UnityObjects;
using Managers;
using UnityEngine;

namespace Controllers.EnemyDrone
{
    public class EnemyDroneGunController : MonoBehaviour
    {
        [Header("Firing Settings")]
        [SerializeField] private List<Transform> firePoints;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float fireRate;
        
        [SerializeField] private SoundDataScriptable soundData;
        private float _timer;
        
        private Queue<GameObject> _bullets;
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

        public void Fire()
        {    
            _timer += Time.deltaTime;

            if (_timer > fireRate)
            {
                if (_bullets.Count > 0)
                {
                    foreach (var firePoint in firePoints)
                    {
                        GameObject bullet = _bullets.Dequeue();
                        bullet.SetActive(true);
                        bullet.transform.position = firePoint.position;
                        bullet.transform.rotation = firePoint.rotation;
                        SoundManager.PLaySound(soundData,"LaserGun");
                        bullet.GetComponent<Rigidbody>().velocity = firePoint.transform.up * 50;
                        ReturnToPool(bullet).Forget();
                    }
                    _timer = 0;
                }
            }
        }

        private async UniTaskVoid ReturnToPool(GameObject bullet)
        {
            await UniTask.Delay(1000);
            if(bullet.activeSelf)
                bullet.SetActive(false);
            _bullets.Enqueue(bullet);
        }
    }
}