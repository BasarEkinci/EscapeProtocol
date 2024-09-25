using DG.Tweening;
using UnityEngine;

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
        private Tween[] _tweens = new Tween[3];
        private void Start()
        {
            _screenWidth = Screen.width;
            _screenHeight = Screen.height;
        }
        public void Back()
        {
            _tweens[0] = panelsParent.GetComponent<RectTransform>().DOAnchorPos(Vector2.zero, duration);
        }
        
        public void Credit()
        {
            _tweens[1] = panelsParent.GetComponent<RectTransform>().DOAnchorPos(Vector2.down * _screenHeight, duration);
        }

        public void Options()
        {
            _tweens[2] =panelsParent.GetComponent<RectTransform>().DOAnchorPos(Vector2.left * _screenWidth, duration);
        }

        public void Exit()
        {
            Application.Quit();
        }

        private void OnDisable()
        {
            foreach (var tween in _tweens)
            {
                tween?.Kill();
            }
        }
    }
}
