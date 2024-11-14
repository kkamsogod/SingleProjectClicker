using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Player player;
    public Monster monster;
    private GameObject inGameObject;

    public event Action OnGameStart;
    public event Action OnGamePause;
    public event Action OnGameResume;
    public event Action OnGameRestart;
    public event Action OnGameQuit;
    public event Action OnMonsterSpawned;

    private bool isGamePaused = false;

    private void Start()
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

    public void NotifyMonsterSpawned(Monster spawnedMonster)
    {
        monster = spawnedMonster;
        OnMonsterSpawned?.Invoke();
    }
}