using System;
using System.Collections.Generic;
using Audio;
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
        [SerializeField] private Slider uiVolumeSlider;

        private Tween[] _tweens = new Tween[5];

        private void Start()
        {
            _tweens[0] = pausePanel.GetComponent<RectTransform>().DOAnchorPos(Vector2.up * Screen.height, 0);
        }

        private void OnEnable()
        {
            MasterVolumeChanged(AudioDataSaver.Instance.GetVolume("MasterVolume"));
            MusicVolumeChanged(AudioDataSaver.Instance.GetVolume("MusicVolume"));
            SfxVolumeChanged(AudioDataSaver.Instance.GetVolume("SFXVolume"));
            UIVolumeChanged(AudioDataSaver.Instance.GetVolume("UIVolume"));
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
                        
            masterVolumeSlider.value = AudioDataSaver.Instance.GetVolume("MasterVolume");
            musicVolumeSlider.value = AudioDataSaver.Instance.GetVolume("MusicVolume");
            sfxVolumeSlider.value = AudioDataSaver.Instance.GetVolume("SFXVolume");
            uiVolumeSlider.value = AudioDataSaver.Instance.GetVolume("UIVolume");
            
            
            _tweens[1] = gamePanel.GetComponent<RectTransform>().DOAnchorPos(Vector2.down * Screen.height, 0.5f);
            _tweens[2] = pausePanel.GetComponent<RectTransform>().DOAnchorPos(Vector2.zero, 0.5f);
        }
        
        private void ResumeGame()
        {
            _tweens[3] = gamePanel.GetComponent<RectTransform>().DOAnchorPos(Vector2.zero, 0.5f);
            _tweens[4] = pausePanel.GetComponent<RectTransform>().DOAnchorPos(Vector2.up * Screen.height, 0.5f);        
        }
        
        public void MasterVolumeChanged(float volume)
        {
            audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
            AudioDataSaver.Instance.SaveVolume("MasterVolume", volume);
        }
        
        public void MusicVolumeChanged(float volume)
        {
            audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
            AudioDataSaver.Instance.SaveVolume("MusicVolume", volume);
        }
        
        public void SfxVolumeChanged(float volume)
        {
            audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
            AudioDataSaver.Instance.SaveVolume("SFXVolume", volume);
        }
        
        public void UIVolumeChanged(float volume)
        {
            audioMixer.SetFloat("UIVolume", Mathf.Log10(volume) * 20);
            AudioDataSaver.Instance.SaveVolume("UIVolume", volume);
        }
        
        public void ExitGame()
        {
            Debug.Log("Game Exited");
        }

        private void OnDisable()
        {
            foreach (var _tween in _tweens)
            {
                _tween?.Kill();
            }
        }
    }
}
