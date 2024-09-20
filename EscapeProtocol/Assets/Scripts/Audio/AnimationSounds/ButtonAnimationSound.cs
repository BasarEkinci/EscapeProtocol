using UnityEngine;

namespace Audio.AnimationSounds
{
    public class ButtonSounds : MonoBehaviour
    {
        [SerializeField] private AudioClip highlightSound;
        [SerializeField] private AudioClip pressedSound;
        
        private AudioSource _audioSource;
        
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }
        
        public void Highlight()
        {
            _audioSource.PlayOneShot(highlightSound);
        }
        
        public void Pressed()
        {
            _audioSource.PlayOneShot(pressedSound);
        }
    }
}
