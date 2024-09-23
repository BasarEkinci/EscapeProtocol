using DG.Tweening;
using Managers;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Controllers.UI
{
    public class GameSceneUIController : MonoBehaviour
    {
        [SerializeField] private GameObject pausePanel;
        [SerializeField] private GameObject gamePanel;
        [SerializeField] private AudioMixer audioMixer;
        
        [SerializeField] private Slider masterVolumeSlider;
        [SerializeField] private Slider musicVolumeSlider;
        [SerializeField] private Slider sfxVolumeSlider;
        
        private void Start()
        {
            pausePanel.GetComponent<RectTransform>().DOAnchorPos(Vector2.up * Screen.height, 0);
        }

        private void OnEnable()
        {
            MasterVolumeChanged(PlayerPrefs.GetFloat("MasterVolume"));
            MusicVolumeChanged(PlayerPrefs.GetFloat("MusicVolume"));
            SfxVolumeChanged(PlayerPrefs.GetFloat("SFXVolume"));

        }

        private void Update()
        {
            if (SceneManager.GetSceneByBuildIndex(1).isLoaded)
            {
                if (GameManager.Instance.IsGamePaused)
                {
                    PauseGame();
                }
                else
                {
                    ResumeGame();
                }
            }
        }
        
        private void PauseGame()
        { 
                        
            masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
            musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume");
            
            gamePanel.GetComponent<RectTransform>().DOAnchorPos(Vector2.down * Screen.height, 0.5f);
            pausePanel.GetComponent<RectTransform>().DOAnchorPos(Vector2.zero, 0.5f);
        }
        
        private void ResumeGame()
        { 
            gamePanel.GetComponent<RectTransform>().DOAnchorPos(Vector2.zero, 0.5f);
            pausePanel.GetComponent<RectTransform>().DOAnchorPos(Vector2.up * Screen.height, 0.5f);        
        }
        
        public void MasterVolumeChanged(float volume)
        {
            audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("MasterVolume", volume);
            PlayerPrefs.Save();
        }
        
        
        public void MusicVolumeChanged(float volume)
        {
            audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("MusicVolume", volume);
            PlayerPrefs.Save();
        }
        
        public void SfxVolumeChanged(float volume)
        {
            audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("SFXVolume", volume);
            PlayerPrefs.Save();
        }
        public void ExitGame()
        {
            Debug.Log("Game Exited");
        }
    }
}
