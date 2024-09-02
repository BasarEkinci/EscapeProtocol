using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenuUIController : MonoBehaviour
    {
        public void Play()
        {
            SceneManager.LoadSceneAsync(sceneBuildIndex: 1);
        }

        public void Options()
        {
            Debug.Log("Options button pressed");
        }

        public void Exit()
        {
            Debug.Log("Exit button pressed");
        }
    }
}
