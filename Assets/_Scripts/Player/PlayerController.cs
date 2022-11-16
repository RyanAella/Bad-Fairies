using System;
using UnityEngine;

namespace _Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
    
        private PlayerStats _stats;

        private void Awake()
        {
            // Instantiate(transform, new Vector3(115, 1, -65), Quaternion.identity);
        }


        private void Start()
        {
            _stats = new PlayerStats(10, 10, 10, 3);
        }
    }
}
