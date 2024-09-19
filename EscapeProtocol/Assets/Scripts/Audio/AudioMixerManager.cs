using UnityEngine;
using UnityEngine.Audio;
using Utilities;

namespace Audio
{
    public class AudioMixerManager : MonoSingleton<AudioMixerManager>
    {
        [SerializeField] private AudioMixer audioMixer;
        private void OnEnable()
        {
            SetMasterVolume(PlayerPrefs.GetFloat("MasterVolume"));
            SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume"));
            SetSfxVolume(PlayerPrefs.GetFloat("SFXVolume"));
        }

        public void SetMasterVolume(float volume)
        {
            audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
            SaveVolume("MasterVolume", volume);
        }
        
        public void SetMusicVolume(float volume)
        {
            audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
            SaveVolume("MusicVolume", volume);
        }
        
        public void SetSfxVolume(float volume)
        {
            audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
            SaveVolume("SFXVolume", volume);
        }
        
        public void SaveVolume(string key, float volume)
        {
            PlayerPrefs.SetFloat(key, volume);
            PlayerPrefs.Save();
        }
    }
}
