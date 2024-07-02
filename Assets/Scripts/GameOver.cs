using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameManager gameManager;  


    public void PlayAgain()
    {
        if (gameManager != null)
        {
            gameManager.HideGameOverPopupAndReset();
            gameManager.RestartGame();
        }
    }

    public void Home()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
