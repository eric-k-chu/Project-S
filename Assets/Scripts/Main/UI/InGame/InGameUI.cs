using UnityEngine;

public class InGameUI : MonoBehaviour
{
    public GameObject enemyOverlay;


    private void Start()
    {
        UIManager.instance.onPauseUIStart += DisableUI;
        UIManager.instance.onPauseUIExit += EnableUI;

        GameEventManager.instance.onBossCutsceneStart += DisableUI;
        GameEventManager.instance.onBossCutsceneEnd += EnableUI;
        GameEventManager.instance.onBossCutsceneEnd += EnableEnemyOverlay;

        GameEventManager.instance.onBossDeathCutsceneEnd += DisableEnemyOverlay;
        GameEventManager.instance.onBossDeathCutsceneEnd += EnableUI;

        GameEventManager.instance.onPortStoneEncounter += DisableUI;

        enemyOverlay.SetActive(false);
    }

    private void OnEnable()
    {
        UIManager.inGame = true;   
    }

    private void OnDisable()
    {
        UIManager.inGame = false;
    }

    private void DisableUI()
    {
        gameObject.SetActive(false);
    }

    private void EnableUI()
    {
        gameObject.SetActive(true);
    }

    private void EnableEnemyOverlay()
    {
        enemyOverlay.SetActive(true);
    }

    private void DisableEnemyOverlay()
    {
        enemyOverlay.SetActive(false);
    }

    private void OnDestroy()
    {
        UIManager.instance.onPauseUIStart -= DisableUI;
        UIManager.instance.onPauseUIExit -= EnableUI;

        GameEventManager.instance.onBossCutsceneStart -= DisableUI;
        GameEventManager.instance.onBossCutsceneEnd -= EnableUI;
        GameEventManager.instance.onBossCutsceneEnd -= EnableEnemyOverlay;

        GameEventManager.instance.onBossDeathCutsceneEnd -= DisableEnemyOverlay;
        GameEventManager.instance.onBossDeathCutsceneEnd -= EnableUI;

        GameEventManager.instance.onPortStoneEncounter -= DisableUI;
    }
}
