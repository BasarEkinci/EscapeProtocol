using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SceneManagement
{
    public class AsyncLoader : MonoBehaviour
    {
        [Header("Screens")]
        [SerializeField] private GameObject mainMenuScreen;
        [SerializeField] private GameObject loadingScreen;
        
        [Header("Slider")]
        [SerializeField] private Slider loadingSlider;
        
        [Header("Text")]
        [SerializeField] private TMP_Text loadingText;

        private float _currentTime;
        
        private void Start()
        {
            loadingText.text = "Loading...";
        }

        private void Update()
        {
            if (loadingScreen.activeSelf)
            {
                LoadingTextAnimation();
            }
        }

        public void LoadSceneAsync(string sceneName)
        {
            mainMenuScreen.SetActive(false);
            loadingScreen.SetActive(true);
            
            StartCoroutine(LoadSceneAsyncCoroutine(sceneName));
        }
        
        IEnumerator LoadSceneAsyncCoroutine(string sceneName)
        {
            AsyncOperation asyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
            
            while (asyncOperation != null && !asyncOperation.isDone)
            {
                float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
                loadingSlider.value = progress;
                
                yield return null;
            }
        }

        private void LoadingTextAnimation()
        {
            _currentTime += Time.deltaTime;
            float time = Mathf.RoundToInt(_currentTime);
            loadingText.text = (time % 2) switch
            {
                0 => "Loading.",
                1 => "Loading..",
                2 => "Loading...",
                _ => loadingText.text
            };
        }
    }
}