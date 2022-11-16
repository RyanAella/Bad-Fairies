using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts
{
    public class MainMenuController : MonoBehaviour
    {
        // UI
        [SerializeField] private Canvas mainCanvas;
        // [SerializeField] private Canvas sceneSelectCanvas;

        public void NewGame()
        {
            SceneManager.LoadScene("LevelDesignDemo");
        }

        // public void SelectLevel()
        // {
        //     mainCanvas.enabled = false;
        //     sceneSelectCanvas.enabled = true;
        // }

        public void CloseGame()
        {
            Application.Quit();
        }

        // public void LoadLevel1()
        // {
        //     SceneManager.LoadScene("Level 1");
        // }

        // public void LoadLevel2()
        // {
        //     SceneManager.LoadScene("Level 2");
        // }

        // public void Return()
        // {
        //     sceneSelectCanvas.enabled = false;
        //     mainCanvas.enabled = true;
        // }
    }
}