using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    public class AudioMixerManager : MonoBehaviour
    {
        [SerializeField] private AudioMixer audioMixer;
        private void OnEnable()
        {
            SetMasterVolume(AudioDataSaver.Instance.GetVolume("MasterVolume"));
            SetMusicVolume(AudioDataSaver.Instance.GetVolume("MusicVolume"));
            SetSfxVolume(AudioDataSaver.Instance.GetVolume("SFXVolume"));
        }

        public void SetMasterVolume(float volume)
        {
            audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
            AudioDataSaver.Instance.SaveVolume("MasterVolume", volume);
        }
        
        public void SetMusicVolume(float volume)
        {
            audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
            AudioDataSaver.Instance.SaveVolume("MusicVolume", volume);
        }
        
        public void SetSfxVolume(float volume)
        {
            audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
            AudioDataSaver.Instance.SaveVolume("SFXVolume", volume);
        }
        
        public void SetUIVolume(float volume)
        {
            audioMixer.SetFloat("UIVolume", Mathf.Log10(volume) * 20);
            AudioDataSaver.Instance.SaveVolume("UIVolume", volume);
        }
    }
}
