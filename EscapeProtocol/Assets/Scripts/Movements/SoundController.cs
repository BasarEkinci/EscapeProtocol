using UnityEngine;

namespace Movements
{
    public class SoundController : MonoBehaviour
    {
        [SerializeField] private AudioClip backStepSound;
        [SerializeField] private AudioClip fallSound;
        
        [SerializeField] private AudioClip footStepSound1;
        [SerializeField] private AudioClip footStepSound2;
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponentInParent<AudioSource>();
        }
        
        public void BackStepSound()
        {
            _audioSource.PlayOneShot(backStepSound);
        }
        
        public void FootStepSound1()
        {
            _audioSource.PlayOneShot(footStepSound1);
        }
        
        public void FootStepSound2()
        {
            _audioSource.PlayOneShot(footStepSound2);
        }
        
        public void FallSound()
        {
            _audioSource.PlayOneShot(backStepSound);
        }
    }
}