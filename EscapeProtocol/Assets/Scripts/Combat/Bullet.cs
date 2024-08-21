using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Combat
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private ParticleSystem hitEffect;
        private void OnCollisionEnter(Collision other)
        {
            IDamageable enemy = other.collider.GetComponent<IDamageable>();
            if (enemy != null)
            {
                Delay(other.GetContact(0).point).Forget();
                enemy.TakeDamage(10);
            }
            else
            {
                Delay(other.GetContact(0).normal).Forget();
            }
        }
        private async UniTaskVoid Delay(Vector3 collisionPoint)
        {
            if (!hitEffect.isPlaying)
            {
                hitEffect.transform.position = collisionPoint;
                hitEffect.Play();
            }
            await UniTask.Delay(100);
            gameObject.SetActive(false);
        }
    }
}
