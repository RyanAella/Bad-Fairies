using _Scripts.Bots;
using _Scripts.Player;
using _Scripts.UI;
using UnityEngine;

namespace _Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject bot;
        [SerializeField] private GameObject player;
        [SerializeField] private Canvas victoryScreen;
        [SerializeField] private GameObject camera;

        private GameObject _newBot;
        private Vector3 _spawnPosition;

        private int _randomSpawnLocation;
        private float _randomXPosition, _randomZPosition;

        // [SerializeField] private GameObject player;
        private static GameManager gameManager { get; set; }

        // Start is called before the first frame update
        void Awake()
        {
            if (gameManager != null && gameManager != this)
            {
                Destroy(this);
            }
            else
            {
                gameManager = this;
            }
            // victoryScreen.enabled = false;
            // camera.SetActive(false);
            // Instantiate(player, new Vector3(115, 1, -65), Quaternion.identity);
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // InvokeRepeating("SpawnNewBot", 0.1f, 2f);
        }

        // private void Update()
        // {
        //     if (BotController.GetBotCounter() == 0)
        //     {
        //         WinOrLoose(true);
        //     }
        //     if (player.gameObject.GetComponent<PlayerController>().Stats.CurrentHealth <= 0)
        //     {
        //         WinOrLoose(false);
        //     }

        // }

        // private void SpawnNewBot() {
        //     _randomSpawnLocation = Random.Range(0, 4);
        // }

        // public void WinOrLoose(bool win)
        // {
        //     victoryScreen.enabled = true;
        //     camera.SetActive(true);

        //     VictoryBehaviour.SetMessage(win);
        //     Time.timeScale = 0;
        // }
    }
}
