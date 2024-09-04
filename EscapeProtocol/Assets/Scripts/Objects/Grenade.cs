using Combat;
using Data.UnityObjects;
using Managers;
using UnityEngine;

namespace Objects
{
    public class Grenade : MonoBehaviour
    {
        [SerializeField] private GameObject explosionEffect;
        [SerializeField] private SoundDataScriptable soundData;
        [SerializeField] private float delay = 3f;
        [SerializeField] private float explosionRadius = 5f;
        
        private float _countdown;
        private bool _hasExploded = false;
        private void OnEnable()
        {
            _countdown = delay;
        }

        private void Update()
        {
            _countdown -= Time.deltaTime;
            if (_countdown <= 0 && !_hasExploded)
            {
                Explode();
                _hasExploded = true;
            }

            if (_hasExploded)
            {
                Destroy(gameObject,0.1f);
            }
        }

        private void Explode()
        { 
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

            foreach (var objectCollider in colliders)
            {
                var damageable = objectCollider.GetComponent<IDamageable>();
                if (damageable != null && objectCollider.CompareTag("Enemy"))
                {
                    damageable.TakeDamage(20);
                }
            }
            SoundManager.PLaySound(soundData,"Grenade");
            CinemachineShake.Instance.ShakeCamera(1,0.2f);
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
        }
    }
}