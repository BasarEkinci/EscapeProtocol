using Combat;
using UnityEngine;

namespace Objects.Combat
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private GameObject hitEffect;

        private void OnCollisionEnter(Collision other)
        {
            Vector3 hitPoint = other.GetContact(0).point;
            Instantiate(hitEffect, hitPoint, Quaternion.identity);
            Debug.Log(other.collider.name);
            IDamageable enemy = other.collider.GetComponent<IDamageable>();
            if(enemy != null)
            {
                enemy.TakeDamage(10);
                gameObject.SetActive(false);
            }
        }
    }
}
