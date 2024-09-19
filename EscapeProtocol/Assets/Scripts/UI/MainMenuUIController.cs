using DG.Tweening;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenuUIController : MonoBehaviour
    {
        [SerializeField] private GameObject panelsParent;
        [SerializeField] private GameObject mainMenuPanel;
        [SerializeField] private GameObject creditPanel;
        [SerializeField] private GameObject optionsPanel;

        [SerializeField] private float duration;

        private float _screenWidth;
        private float _screenHeight;
        private void Start()
        {
            _screenWidth = Screen.width;
            _screenHeight = Screen.height;
        }

        public void Play()
        {
            GameManager.Instance.StartGame();
            SceneManager.LoadScene(sceneBuildIndex: 1);
        }

        public void Back()
        {
            panelsParent.GetComponent<RectTransform>().DOAnchorPos(Vector2.zero, duration);
        }
        
        public void Credit()
        {
            panelsParent.GetComponent<RectTransform>().DOAnchorPos(Vector2.down * _screenHeight, duration);
        }

        public void Options()
        {
            panelsParent.GetComponent<RectTransform>().DOAnchorPos(Vector2.left * _screenWidth, duration);
        }

        public void Exit()
        {
        }
    }
}
