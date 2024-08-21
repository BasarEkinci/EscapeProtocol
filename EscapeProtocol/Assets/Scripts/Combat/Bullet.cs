using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Combat
{
    public class Bullet : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            IDamageable enemy = other.collider.GetComponent<IDamageable>();
            if (enemy != null)
            {
                Delay().Forget();
                enemy.TakeDamage(10);
            }
            else
            {
                Delay().Forget();
            }
        }
        private async UniTaskVoid Delay()
        {
            await UniTask.Delay(300);
            gameObject.SetActive(false);
        }
    }
}
