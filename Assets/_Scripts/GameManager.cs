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

        private GameObject _newBot;
        private Vector3 _spawnPosition;

        private int _randomSpawnLocation;
        private float _randomXPosition, _randomZPosition;

        public static GameManager instance { get; private set; }

        void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this);
            }
            else
            {
                instance = this;
            }
            // victoryScreen.enabled = false;
            // camera.SetActive(false);
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

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
        //     
        // }
    }
}
