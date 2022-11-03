using UnityEngine;

namespace _Scripts
{
    public class GameManager : MonoBehaviour
    {
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
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
