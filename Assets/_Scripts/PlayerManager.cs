using System;
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

        private void Start()
        {
            Instantiate(player, new Vector3(115, 1, -65), Quaternion.identity);
        }
    }
}
