using UnityEngine;

namespace _Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        private enum PlayerMode
        {
            Default, // player can walk, run, jump and attack enemies
            BuildMode, // player can walk, jump and build
            Die // player life == 0, destroy gameobject
        }

        [SerializeField] private GameObject gameManager;

        // player behaviour
        private PlayerMode _mode = PlayerMode.Default;

        public PlayerStats Stats;

        private bool _buildingActive = false;

        private int _playerMode = 0;

        private void Start()
        {
            Stats = new PlayerStats(50, 50, 10, 3);
        }

        private void Update()
        {
            UpdatePlayerMode();

            switch (_mode)
            {
                case PlayerMode.Default:
                    // Default();
                    break;
                case PlayerMode.BuildMode:
                    // Building();
                    break;
                case PlayerMode.Die:
                    Die();
                    break;
            }
        }

        /**
         * Sets PlayerMode according to defined criteria.
         * Sets PlayerMode according to Input or player life.
         */
        private void UpdatePlayerMode()
        {
            var pos = transform.position;

            // Check if Player is dead
            if (Stats.CurrentHealth <= 0)
            {
                _mode = PlayerMode.Die;
                _playerMode = 2;
                Die();
                return;
            }

            if (!_buildingActive)
            {
                _mode = PlayerMode.Default;
                _playerMode = 0;
            }

            if (_playerMode == 0 && (!_buildingActive && Input.GetKeyDown(KeyCode.B)))
            {
                _mode = PlayerMode.BuildMode;
                _playerMode = 1;
                _buildingActive = true;
                return;
            }

            if (_playerMode == 1 && (_buildingActive && Input.GetKeyDown(KeyCode.B)))
            {
                _mode = PlayerMode.Default;
                _playerMode = 0;
                _buildingActive = false;
            }
        }

        private void Building()
        {
            // run speed deactivated
            // weapon deactivated
        }

        private void Default()
        {
            // run speed activated
            // weapon activated
        }

        private void Die()
        {
            Destroy(gameObject);
        }

        public void TakeDamage(int dmg)
        {
            if (Stats.CurrentHealth > 0)
            {
                Stats.TakeDamage(dmg);
            }
        }

        public int GetCurrentPlayerMode()
        {
            return _playerMode;
        }
    }
}