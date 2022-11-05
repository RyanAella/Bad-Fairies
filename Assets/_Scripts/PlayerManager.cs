using UnityEngine;

namespace _Scripts
{
    public class PlayerManager : MonoBehaviour
    {
        #region Singleton

        public static PlayerManager instance;

        private void Awake()
        {
            instance = this;
        }

        #endregion

        public GameObject player;
    }
}
