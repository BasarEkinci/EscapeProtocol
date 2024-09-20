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
        public GameScene CurrentScene => _currentScene;
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
            Debug.Log(_currentScene);
        }

        public void StartGame()
        {
            _currentScene = GameScene.GameScene;
        }
        public void EndGame()
        {
            _currentScene = GameScene.MainMenuScene;
        }
        
        private void PauseGame()
        {
            _isGamePaused = true;
        }

        private void ResumeGame()
        {
            _isGamePaused = false;
        }
    }
}