using _Scripts.Bots;
using _Scripts.Player;
using _Scripts.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject bot;
        [SerializeField] private GameObject player;
        [SerializeField] private Canvas pauseScreen;
        [SerializeField] private Canvas ingameScreen;

        public static bool _gamePaused;

        public static GameManager Instance { get; private set; }

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }

            _gamePaused = false;
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseGame();
            }
        }

        public void PauseGame()
        {
            if (!_gamePaused)
            {
                // pause game
                Time.timeScale = 0;
                pauseScreen.enabled = true;
                ingameScreen.enabled = false;

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                _gamePaused = true;
            }
            else
            {
                // resume game
                Time.timeScale = 1;
                pauseScreen.enabled = false;
                ingameScreen.enabled = true;

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                _gamePaused = false;
            }
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}
