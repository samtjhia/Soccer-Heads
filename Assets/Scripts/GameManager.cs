using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int playerScore = 0, aiScore = 0;
    public TextMeshProUGUI scoreText;
    public string score;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float timer;
    public GameObject gameOverPopup;
    public TextMeshProUGUI gameOverMessage;
    public TextMeshProUGUI winnerText;
    private bool isGameOver = false;

    void Update()
    {

        if (!isGameOver && Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        if (!isGameOver)
        {
            UpdateTimer();
            UpdateScore();

            if (timer <= 0)
            {
                EndGame();
            }
        }
    }

    void UpdateTimer()
    {
        timer -= Time.deltaTime;
        timerText.text = FormatTime(timer);
    }

    void UpdateScore()
    {
        
        scoreText.text = playerScore.ToString() + " - " + aiScore.ToString();
    }

    void EndGame()
    {
        isGameOver = true;
        ShowGameOverPopup();

        string winner;
        if (playerScore > aiScore)
        {
            winner = "Player 1";
        }
        else if (aiScore > playerScore)
        {
            winner = "Player 2";
        }
        else
        {
            winner = "Tie";
        }

        winnerText.text = (winner == "Tie") ? "It's a Tie!" : "Winner: " + winner;
    }

    void ShowGameOverPopup()
    {
        gameOverMessage.text = "Game Over!";
        gameOverPopup.SetActive(true);
        SoundManager.Instance.PlayGameOverMusic();
    }


    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PauseManager pauseManager = FindObjectOfType<PauseManager>();
        if (pauseManager != null)
        {

            pauseManager.ResetState();
        }

    }

    public void HideGameOverPopupAndReset()
    {
        gameOverPopup.SetActive(false);
        isGameOver = false;
        SoundManager.Instance.PlayGameplayMusic();

        PauseManager pauseManager = FindObjectOfType<PauseManager>();
        if (pauseManager != null)
        {

            pauseManager.ResetState();
        }

    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void TogglePause()
    {
        PauseManager pauseManager = FindObjectOfType<PauseManager>();

        if (pauseManager != null)
        {
            pauseManager.PauseGame();
        }
    }
}
