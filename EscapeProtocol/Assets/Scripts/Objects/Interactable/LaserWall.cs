using Combat;
using Data.UnityObjects;
using Managers;
using UnityEngine;

namespace Objects.Interactable
{
    public class LaserWall : MonoBehaviour
    {
        [SerializeField] private SoundDataScriptable soundData;
        [SerializeField] private GameObject hitParticle;
        private float _nextDamageTime = 0;
        private float _damageInterval = 1.5f;
        private void OnCollisionEnter(Collision other)
        {
            IDamageable player = other.gameObject.GetComponent<IDamageable>();
            player?.TakeDamage(30);
            CinemachineShake.Instance.ShakeCamera(4,0.1f);
            HitEvents(other.GetContact(0).point);
        }


        private void OnCollisionStay(Collision other)
        {
            IDamageable player = other.gameObject.GetComponent<IDamageable>();
            if (Time.time > _nextDamageTime)
            {
                CinemachineShake.Instance.ShakeCamera(5,0.1f);
                player?.TakeDamage(30);
                HitEvents(other.GetContact(0).point);
                _nextDamageTime = Time.time + _damageInterval;
            }
        }

        private void HitEvents(Vector3 position)
        {
            SoundManager.PLaySound(soundData, "LaserWallHit");
            Instantiate(hitParticle,position + Vector3.up,transform.rotation);
        }
    }
}
