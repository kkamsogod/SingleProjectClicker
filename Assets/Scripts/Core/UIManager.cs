using System;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public event Action OnPopupOpened;
    public event Action OnPopupClosed;

    private GameObject inGameUI;

    [SerializeField] private GameObject titleSceneUI;
    [SerializeField] private GameObject gameSceneUI;

    private void Start()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        inGameUI = Instantiate(Resources.Load<GameObject>("UIInGame"));
    }

    public void Show(GameObject UI)
    {
        if (UI != null && !UI.activeSelf)
        {
            UI.SetActive(true);
            OnPopupOpened?.Invoke();
        }
    }

    public void Hide(GameObject UI)
    {
        if (UI != null && UI.activeSelf)
        {
            UI.SetActive(false);
            OnPopupClosed?.Invoke();
        }
    }
}