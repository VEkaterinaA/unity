using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameMenuController
    {
        public GameObject CanvasMenu;

        public void GameMenuClick()
        {
            CanvasMenu.SetActive(true); 
            Time.timeScale = 0;
        }

        public void ContinueClick()
        {
            CanvasMenu.SetActive(false);
            Time.timeScale = 1;
        }
        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
            Time.timeScale = 1;
        }
        public void Option()
        {

            Time.timeScale = 1;
        }
        public void ExitInMenu()
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1;
        }


    }
}
