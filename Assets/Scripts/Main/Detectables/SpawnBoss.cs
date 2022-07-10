using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameEventManager.instance.StartBossEncounter(other.gameObject.transform);
        GameEventManager.instance.StartBossCutscene();
        AudioManager.instance.PlayBossTheme();
    }

    private void OnTriggerExit()
    {
        Destroy(gameObject);
    }
}
