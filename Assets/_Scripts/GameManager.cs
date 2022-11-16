using UnityEngine;

namespace _Scripts
{
    public class GameManager : MonoBehaviour
    {

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
            // Instantiate(player, new Vector3(115, 1, -65), Quaternion.identity);
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
