using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Combat
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private ParticleSystem impactEffect;
        private void OnCollisionEnter(Collision other)
        {
            IDamageable enemy = other.collider.GetComponent<IDamageable>();
            DamageEffect(other.GetContact(0).point).Forget();
            if (enemy != null)
            {
                DamageEffect(transform.position).Forget();
                enemy.TakeDamage(10);
            }
        }
        private async UniTaskVoid DamageEffect(Vector3 hitPoints)
        {
            impactEffect.transform.position = hitPoints;
            if (impactEffect.isStopped)
                impactEffect.Play();
            await UniTask.Delay(300);
            gameObject.SetActive(false);
        }
    }
}
