using Combat;
using UnityEngine;

namespace Objects.Interactable
{
    public class LaserWall : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            IDamageable player = other.gameObject.GetComponent<IDamageable>();

            if (player != null)
            {
                player.TakeDamage(30);
                //Damage Sound
            }
        }
    }
}
