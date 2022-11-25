using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        public enum PlayerMode
        {
            Default, // player can walk, run, jump and attack enemies
            BuildMode, // player can walk, jump and build
            Die // player life == 0, destroy gameobject
        }

        // player instance
        public static GameObject Instance { get; private set; }

        // player behaviour
        private PlayerMode _mode = PlayerMode.Default;

        [HideInInspector]
        public Stats m_stats;

        private bool _buildingActive = false;

        private void Awake()
        {
            if (Instance != null && Instance != gameObject)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = gameObject;
            }

            m_stats = GetComponent(typeof(Stats)) as Stats;
        }

        private void Update()
        {
            if (!GameManager._gamePaused)
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
        }

        /**
         * Sets PlayerMode according to defined criteria.
         * Sets PlayerMode according to Input or player life.
         */
        private void UpdatePlayerMode()
        {
            var pos = transform.position;

            // Check if Player is dead
            if (m_stats.CurrentHealth <= 0)
            {
                _mode = PlayerMode.Die;
                Die();
                return;
            }

            // If Building Mode is not active
            if (!_buildingActive)
            {
                _mode = PlayerMode.Default;
            }

            // If player is in Default Mode and B is pressed, activate BuildMode
            if (_mode == PlayerMode.Default && (!_buildingActive && Input.GetKeyDown(KeyCode.B)))
            {
                _mode = PlayerMode.BuildMode;
                _buildingActive = true;
                return;
            }

            // If Build Mode is active and B is pressed, activate Default Mode
            if (_mode == PlayerMode.BuildMode && (_buildingActive && Input.GetKeyDown(KeyCode.B)))
            {
                _mode = PlayerMode.Default;
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

        // Player takes damage
        public void TakeDamage(int dmg)
        {
            if (m_stats.CurrentHealth > 0)
            {
                m_stats.TakeDamage(dmg);
            }
        }

        // Returns the current player mode
        public PlayerMode GetPlayerMode()
        {
            return _mode;
        }
    }
}