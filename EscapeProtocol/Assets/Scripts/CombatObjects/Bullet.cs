using UnityEngine;

namespace CombatObjects
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private ParticleSystem impactEffect;
        
        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision other)
        {
            Vector3 hitPoints = other.GetContact(0).point;
            impactEffect.transform.position = hitPoints;
            
            if (impactEffect.isStopped)
                impactEffect.Play();
        }
    }
}
