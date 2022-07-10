using UnityEngine;
using System;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private void Awake()
    {
        instance = this;
    }

    public static bool inGame;
    public static bool inPause;
    public static bool inOptions;
    public static bool inVictory;
    public static bool inGameOver;

    private void Start()
    {
        inGame = inPause = inOptions = inVictory = inGameOver = false;
    }

    public event Action onPauseUIStart;
    public void TurnOffInGameUI()
    {
        onPauseUIStart?.Invoke();
        inPause = true;
    }

    public event Action onPauseUIExit;
    public void TurnOnInGameUI()
    {
        onPauseUIExit?.Invoke();
        inPause = false;
    }
}
