using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Combat
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private ParticleSystem impactEffect;
        private void OnCollisionEnter(Collision other)
        {
            DamageEffect(other.GetContact(0).point).Forget();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<IDamageable>() != null)
            {
                DamageEffect(transform.position).Forget();
                other.GetComponent<IDamageable>().TakeDamage(10);
            }
        }
        
        private async UniTaskVoid DamageEffect(Vector3 hitPoints)
        {
            impactEffect.transform.position = hitPoints;
            if (impactEffect.isStopped)
                impactEffect.Play();
            await UniTask.Delay(500);
            gameObject.SetActive(false);
        }
    }
}
