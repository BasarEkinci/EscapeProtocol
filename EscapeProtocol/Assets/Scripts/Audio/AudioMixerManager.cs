using UnityEngine;
using UnityEngine.Audio;
using Utilities;

namespace Audio
{
    public class AudioMixerManager : MonoSingleton<AudioMixerManager>
    {
        [SerializeField] private AudioMixer audioMixer;
        
        public void SetMasterVolume(float volume)
        {
            audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        }
        
        public void SetMusicVolume(float volume)
        {
            audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        }
        
        public void SetSfxVolume(float volume)
        {
            audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        }
    }
}
