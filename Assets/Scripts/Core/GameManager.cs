using System;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : Singleton<GameManager>
{
    public Player player;
    public Monster monster;
    public MonsterSpawner monsterSpawner;
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
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "InGameScene")
        {
            InitializeGame();
        }
    }

    private void InitializeGame()
    {
        inGameObject = Instantiate(Resources.Load<GameObject>("GameObjectInGame"));
        player = inGameObject.GetComponentInChildren<Player>();
        monsterSpawner = inGameObject.GetComponentInChildren<MonsterSpawner>();

        OnGameStart?.Invoke();
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
        monsterSpawner.ResetSpawner();
        Time.timeScale = 1;
        isGamePaused = false;
        OnGameRestart?.Invoke();
    }

    public void QuitGame()
    {
        OnGameQuit?.Invoke();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void NotifyMonsterSpawned(Monster spawnedMonster)
    {
        monster = spawnedMonster;
        OnMonsterSpawned?.Invoke();
    }
}