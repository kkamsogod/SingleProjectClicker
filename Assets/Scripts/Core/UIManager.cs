using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    public event Action OnPopupOpened;
    public event Action OnPopupClosed;

    private GameObject currentUI;
    private bool isAnyPanelOpen = false;

    private void Start()
    {
        InitializeUI();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializeUI();
    }

    private void InitializeUI()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == "TitleScene")
        {
            LoadUI("UIInTitle");
        }
        else if (currentSceneName == "InGameScene")
        {
            LoadUI("UIInGame");
        }
    }

    private void LoadUI(string uiPrefabName)
    {
        if (currentUI != null)
        {
            Destroy(currentUI);
        }

        GameObject uiPrefab = Resources.Load<GameObject>(uiPrefabName);
        if (uiPrefab != null)
        {
            currentUI = Instantiate(uiPrefab);
        }        
    }

    public void Show(GameObject UI)
    {
        if (UI != null && !UI.activeSelf)
        {
            UI.SetActive(true);
            isAnyPanelOpen = true;
            OnPopupOpened?.Invoke();
        }
    }

    public void Hide(GameObject UI)
    {
        if (UI != null && UI.activeSelf)
        {
            UI.SetActive(false);
            isAnyPanelOpen = false;
            OnPopupClosed?.Invoke();
        }
    }

    public bool IsAnyPanelOpen() => isAnyPanelOpen;
}