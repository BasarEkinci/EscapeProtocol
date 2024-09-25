using Inputs;
using UnityEngine;
using Utilities;

namespace Managers
{
    public enum GameScene
    {
        MainMenuScene,
        GameScene,
    }
    
    public class GameManager : MonoSingleton<GameManager>
    {
        public bool IsGamePaused => _isGamePaused;
        
        private InputHandler _inputHandler;
        private GameScene _currentScene;
        
        private bool _isGamePaused;
        
        protected override void Awake()
        {
            _inputHandler = new InputHandler();
        }

        private void Start()
        {
            _isGamePaused = false;
        }

        private void Update()
        {
            if (_inputHandler.GetPauseInput())
            {
                if (_isGamePaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }
        private void PauseGame()
        {
            _isGamePaused = true;
            Cursor.visible = true;
        }

        private void ResumeGame()
        {
            _isGamePaused = false;
            Cursor.visible = false;
        }
    }
}