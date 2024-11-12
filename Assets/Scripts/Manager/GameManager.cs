using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Player player;
    private GameObject inGameObject;

    public event Action OnGameStart;
    public event Action OnGamePause;
    public event Action OnGameResume;
    public event Action OnGameRestart;
    public event Action OnGameQuit;

    private bool isGamePaused = false;

    private void Awake()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        inGameObject = Instantiate(Resources.Load<GameObject>("GameObjectInGame"));
        player = inGameObject.GetComponentInChildren<Player>();
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        isGamePaused = false;
        OnGameStart?.Invoke();
    }

    public void PauseGame()
    {
        if (!isGamePaused)
        {
            Time.timeScale = 0;
            isGamePaused = true;
            OnGamePause?.Invoke();
        }
    }

    public void ResumeGame()
    {
        if (isGamePaused)
        {
            Time.timeScale = 1;
            isGamePaused = false;
            OnGameResume?.Invoke();
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        isGamePaused = false;
        OnGameRestart?.Invoke();
    }

    public void QuitGame()
    {
        OnGameQuit?.Invoke();
        Application.Quit();
    }
}