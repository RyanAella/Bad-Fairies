using UnityEngine;

namespace _Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
    
        private PlayerStats _stats;
    

        private void Start()
        {
            _stats = new PlayerStats(10, 10, 10, 3);
        }
    }
}
