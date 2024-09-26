using UnityEngine;
using UnityEngine.Audio;
using Utilities;

namespace Audio
{
    public class AudioMixerManager : MonoBehaviour
    {
        [SerializeField] private AudioMixer audioMixer;
        private void OnEnable()
        {
            SetMasterVolume(DataSaver.Instance.GetDataFloat("MasterVolume"));
            SetMusicVolume(DataSaver.Instance.GetDataFloat("MusicVolume"));
            SetSfxVolume(DataSaver.Instance.GetDataFloat("SFXVolume"));
        }

        public void SetMasterVolume(float volume)
        {
            audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
            DataSaver.Instance.SaveDataFloat("MasterVolume", volume);
        }
        
        public void SetMusicVolume(float volume)
        {
            audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
            DataSaver.Instance.SaveDataFloat("MusicVolume", volume);
        }
        
        public void SetSfxVolume(float volume)
        {
            audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
            DataSaver.Instance.SaveDataFloat("SFXVolume", volume);
        }
        
        public void SetUIVolume(float volume)
        {
            audioMixer.SetFloat("UIVolume", Mathf.Log10(volume) * 20);
            DataSaver.Instance.SaveDataFloat("UIVolume", volume);
        }
    }
}
