using Combat;
using Data.UnityObjects;
using Managers;
using UnityEngine;

namespace Objects.Interactable
{
    public class Mine : MonoBehaviour
    {
        [Header("Sound")]
        [SerializeField] private SoundDataScriptable soundData;
        
        [Header("VFX")]
        [SerializeField] private GameObject explosionParticle;
        
        [Header("Visuals")]
        [SerializeField] private Material mineLight;
        [SerializeField] private Material activeMaterial;
        [SerializeField] private Material inactiveMaterial;
        
        [Header("Settings")]
        [SerializeField] private int damage;

        private void OnEnable()
        {
            mineLight.color = inactiveMaterial.color;
        }

        private void OnTriggerEnter(Collider other)
        {
            IDamageable player = GetDamageable(other);
            if(player != null)
            {
                mineLight.color = Color.Lerp(activeMaterial.color, inactiveMaterial.color, 0.5f);
                SoundManager.PLaySound(soundData,"MineActivate");
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            IDamageable player = GetDamageable(other);
            if (player != null)
            {
                player.TakeDamage(damage);
                CinemachineShake.Instance.ShakeCamera(2,0.2f);
                Instantiate(explosionParticle, transform.position, Quaternion.identity);
                Destroy(gameObject);   
            }
        }
        
        private IDamageable GetDamageable(Collider other)
        {
            return other.GetComponent<IDamageable>();
        }
    }
}
