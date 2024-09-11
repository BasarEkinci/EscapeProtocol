using Combat;
using Controllers.Player;
using UnityEngine;

namespace Objects.Combat
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private GameObject hitEffect;
        [SerializeField] private int damage = 10;

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out Collider coll))
            {
                Instantiate(hitEffect, transform.position, Quaternion.identity);
                IDamageable enemy = other.collider.GetComponent<IDamageable>();
                if(enemy != null)
                {
                    enemy.TakeDamage(damage);
                    gameObject.SetActive(false);
                }    
            }
        }
    }
}
