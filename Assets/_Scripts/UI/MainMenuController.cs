using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.UI
{
    public class MainMenuController : MonoBehaviour
    {
        public void NewGame()
        {
            SceneManager.LoadScene("LevelDesignDemo");
        }

        public void CloseGame()
        {
            Application.Quit();
        }
    }
}