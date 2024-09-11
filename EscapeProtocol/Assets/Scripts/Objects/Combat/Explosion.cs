using Data.UnityObjects;
using Managers;
using UnityEngine;

namespace Objects.Combat
{
    public class Explosion : MonoBehaviour
    {
        [SerializeField] private SoundDataScriptable soundData;
        private AudioSource _audioSource;
        
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            SoundManager.PLaySound(soundData,"Explosion",_audioSource);
        }
    }
}