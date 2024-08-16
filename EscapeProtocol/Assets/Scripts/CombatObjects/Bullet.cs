using UnityEngine;

namespace CombatObjects
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _damage;

        private void Update()
        {
            transform.Translate(Vector3.forward * (_speed * Time.deltaTime));
        }
    }
}
