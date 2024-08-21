using UnityEngine;

namespace Movements
{
    public class PlayerSoundManager : MonoBehaviour
    {
        [SerializeField] private AudioClip footStepSound;
        [SerializeField] private AudioClip footStepSound2;
        [SerializeField] private AudioClip backStepSound;
        [SerializeField] private AudioClip fallSound;
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }
        
        public void BackStepSound()
        {
            _audioSource.PlayOneShot(backStepSound);
        }
        
        public void FallSound()
        {
            _audioSource.PlayOneShot(backStepSound);
        }
    }
}