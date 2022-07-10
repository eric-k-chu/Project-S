using UnityEngine;
using UnityEngine.SceneManagement;

public class ConditionalUI : MonoBehaviour
{
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject victory;
    [SerializeField] private Animator BlackBars;

    private void Start()
    {
        gameOver.SetActive(false);
        victory.SetActive(false);
        GameEventManager.instance.onPlayerDeath += EnableGameOverScreen;

        GameEventManager.instance.onBossCutsceneStart += EnableBlackBars;
        GameEventManager.instance.onBossCutsceneEnd += DisableBlackBars;
        GameEventManager.instance.onBossDeathCutsceneEnd += DisableBlackBars;

        GameEventManager.instance.onPortStoneEncounter += EnableBlackBars;
        GameEventManager.instance.onPortStoneEncounterEnd += DisableBlackBars;
        GameEventManager.instance.onPlayerHasTeleported += EnableVictoryScreen;
    }

    public void EnableGameOverScreen()
    {
        gameOver.SetActive(true);
        Time.timeScale = 0f;
    }

    public void EnableVictoryScreen()
    {
        victory.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartButton(string scene)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(scene);
    }

    public void QuitButton()
    {
        Application.Quit();

        Debug.Log("Quitting Game.");
    }

    public void EnableBlackBars()
    {
        BlackBars.SetTrigger("ShowBars");
    }

    public void DisableBlackBars()
    {
        BlackBars.SetTrigger("HideBars");
    }

    private void OnDestroy()
    {
        GameEventManager.instance.onPlayerDeath -= EnableGameOverScreen;

        GameEventManager.instance.onBossCutsceneStart -= EnableBlackBars;
        GameEventManager.instance.onBossCutsceneEnd -= DisableBlackBars;
        GameEventManager.instance.onBossDeathCutsceneEnd -= DisableBlackBars;

        GameEventManager.instance.onPortStoneEncounter -= EnableBlackBars;
        GameEventManager.instance.onPortStoneEncounterEnd -= DisableBlackBars;

        GameEventManager.instance.onPlayerHasTeleported -= EnableVictoryScreen;
    }
}
