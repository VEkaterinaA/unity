using Assets.Levels.Scripts.Player;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Levels.Scripts
{
    public class GameController : MonoBehaviour
    {
        //Unity gameobjects
        public Button GameMenu;
        public GameObject CanvasMenu;

        //Button menu
        public Button Continue, Restart, Option, ExitInMenu;

        //Classes
        private ScoreText _scoreText;
        private GameMenuController _gameMenuController;

        [Inject]
        private void Construct(ScoreText scoreText, GameMenuController gameMenuController)
        {
            _scoreText = scoreText;
            _gameMenuController = gameMenuController;
        }

        private void Awake()
        {
            GameMenu.onClick.AddListener(_gameMenuController.GameMenuClick);
            Continue.onClick.AddListener(_gameMenuController.ContinueClick);
            Restart.onClick.AddListener(_gameMenuController.Restart);
            Option.onClick.AddListener(_gameMenuController.Option);
            ExitInMenu.onClick.AddListener(_gameMenuController.ExitInMenu);

            _gameMenuController.CanvasMenu = CanvasMenu;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _gameMenuController.GameMenuClick();
            }
        }
        private void OnApplicationQuit()
        {
            PlayerPrefs.SetFloat("Score", _scoreText._Score);
            PlayerPrefs.Save();
        }

    }
}