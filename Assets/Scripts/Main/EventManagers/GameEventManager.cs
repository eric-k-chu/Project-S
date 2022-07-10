using UnityEngine;
using System;

public class GameEventManager : MonoBehaviour
{
    public static GameEventManager instance;

    private void Awake()
    {
        instance = this;
    }

    // Event Variables
    [HideInInspector] public bool hasBossSpawned;

    private void Start()
    {
        hasBossSpawned = false;
    }

    // Cooldowns
    public event Action onDashCooldown;
    public void StartDashCooldown()
    {
        onDashCooldown?.Invoke();
    }

    public event Action onGunRollCooldown;
    public void StartGunRollCooldown()
    {
        onGunRollCooldown?.Invoke();
    }

    public event Action onAirSkillUse;
    public void DecreaseStyleMeter()
    {
        onAirSkillUse?.Invoke();
    }

    // Damage Taken
    public event Action<float> onEnemyDamageTaken;
    public void EnemyTakeDamage(float dmg)
    {
        onEnemyDamageTaken?.Invoke(dmg);
    }

    public event Action<float> onPlayerDamageTaken;
    public void PlayerTakeDamage(float dmg)
    {
        onPlayerDamageTaken?.Invoke(dmg);
    }

    // Boss Encounters

    public event Action<Transform> onBossEncounter;
    public void StartBossEncounter(Transform player)
    {
        onBossEncounter?.Invoke(player);
        hasBossSpawned = true;
    }

    public event Action onBossCutsceneStart;
    public void StartBossCutscene()
    {
        onBossCutsceneStart?.Invoke();
    }

    public event Action onBossCutsceneEnd;
    public void EndBossCutscene()
    {
        onBossCutsceneEnd?.Invoke();
    }

    public event Action onBossDeathCutsceneEnd;
    public void EndBossDeathCutscene()
    {
        onBossDeathCutsceneEnd();
    }

    // Entity Deaths

    public event Action onBossDeath;
    public void EndBossEncounter()
    {
        onBossDeath?.Invoke();
        hasBossSpawned = false;
    }

    public event Action onPlayerDeath;
    public void LoseGame()
    {
        onPlayerDeath?.Invoke();
    }


    // Port Stone
    public event Action onPortStoneEncounter;
    public void StartPortStoneEncounter()
    {
        onPortStoneEncounter?.Invoke();
    }

    public event Action onPortStoneEncounterEnd;
    public void EndPortStoneEncounter()
    {
        onPortStoneEncounterEnd?.Invoke();
    }

    public event Action onPlayerHasTeleported;
    public void WinGame()
    {
        onPlayerHasTeleported?.Invoke();
    }

    // Wall of Death
    public event Action onCompactorEnter;
    public void StartCompactor()
    {
        onCompactorEnter?.Invoke();
    }
}
