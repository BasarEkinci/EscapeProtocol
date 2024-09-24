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
        
    }
}