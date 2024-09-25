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
            MasterVolumeChanged(DataSaver.Instance.GetDataFloat("MasterVolume"));
            MusicVolumeChanged(DataSaver.Instance.GetDataFloat("MusicVolume"));
            SfxVolumeChanged(DataSaver.Instance.GetDataFloat("SFXVolume"));
            UIVolumeChanged(DataSaver.Instance.GetDataFloat("UIVolume"));
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
                        
            masterVolumeSlider.value = DataSaver.Instance.GetDataFloat("MasterVolume");
            musicVolumeSlider.value = DataSaver.Instance.GetDataFloat("MusicVolume");
            sfxVolumeSlider.value = DataSaver.Instance.GetDataFloat("SFXVolume");
            uiVolumeSlider.value = DataSaver.Instance.GetDataFloat("UIVolume");
            
            
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
            DataSaver.Instance.SaveDataFloat("MasterVolume", volume);
        }
        
        public void MusicVolumeChanged(float volume)
        {
            audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
            DataSaver.Instance.SaveDataFloat("MusicVolume", volume);
        }
        
        public void SfxVolumeChanged(float volume)
        {
            audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
            DataSaver.Instance.SaveDataFloat("SFXVolume", volume);
        }
        
        public void UIVolumeChanged(float volume)
        {
            audioMixer.SetFloat("UIVolume", Mathf.Log10(volume) * 20);
            DataSaver.Instance.SaveDataFloat("UIVolume", volume);
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
