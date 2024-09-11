using Combat;
using UnityEngine;

namespace Objects.Combat
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private GameObject hitEffect;
        [SerializeField] private int damage = 10;

        private void OnCollisionEnter(Collision other)
        { 
            Vector3 hitPoint = other.GetContact(0).point;
            Instantiate(hitEffect, hitPoint, Quaternion.identity); 
            var enemy = other.collider.GetComponent<IDamageable>(); 
            if(enemy != null) 
            { 
                enemy.TakeDamage(damage); 
                gameObject.SetActive(false);
            }    
        }
    }
}