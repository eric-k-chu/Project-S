using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    public static bool isPaused = false;
    public static bool toMenu = false;
    public GameObject pauseUI;

    private void Start()
    {
        pauseUI.SetActive(false);
    }

    private void Update()
    {
        if (!UIManager.inGameOver && !UIManager.inOptions && !UIManager.inVictory)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPaused)
                {
                    UIManager.instance.TurnOnInGameUI();
                    resumeGame();
                }

                else
                {
                    UIManager.instance.TurnOffInGameUI();
                    pauseGame();
                }
            }
        }

        if (toMenu)
        {
            resumeGame();
            toMenu = false;
        }
    }

    public void resumeGame()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void pauseGame ()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void loadTitle (string sceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    public void quitGame ()
    {
        Application.Quit();
    }
}
