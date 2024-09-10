using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Controllers.EnemyDrone
{
    public class EnemyDroneGunController : MonoBehaviour
    {
        [Header("Firing Settings")]
        [SerializeField] private List<Transform> firePoints;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float fireRate;

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
                    /*GameObject bullet = _bullets.Dequeue();
                    bullet.SetActive(true);
                    bullet.transform.position = firePoints[0].position;
                    bullet.transform.rotation = firePoints[0].rotation;
                    bullet.GetComponent<Rigidbody>().velocity = firePoints.transform.forward * -50;
                    ReturnToPool(bullet).Forget();*/

                    foreach (var firePoint in firePoints)
                    {
                        GameObject bullet = _bullets.Dequeue();
                        bullet.SetActive(true);
                        bullet.transform.position = firePoint.position;
                        bullet.transform.rotation = firePoint.rotation;
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