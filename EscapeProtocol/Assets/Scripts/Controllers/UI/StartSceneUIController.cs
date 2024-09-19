using UnityEngine;
using UnityEngine.UI;

namespace Controllers.UI
{
    public class StartSceneUIController : MonoBehaviour
    {
        [SerializeField] Slider masterVolumeSlider;
        [SerializeField] Slider musicVolumeSlider;
        [SerializeField] Slider sfxVolumeSlider;
        
        private void Update()
        {
            masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
            musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        }
    }
}
