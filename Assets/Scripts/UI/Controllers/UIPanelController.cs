using UnityEngine;

public class UIPanelController : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject storePanel;

    private void Awake()
    {
        pausePanel.SetActive(false);
        storePanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (UIManager.Instance.IsAnyPanelOpen() && !pausePanel.activeSelf) return;

            TogglePausePanel();
        }
    }

    public void TogglePausePanel()
    {
        if (storePanel.activeSelf || (UIManager.Instance.IsAnyPanelOpen() && !pausePanel.activeSelf)) return;

        bool isCurrentlyActive = pausePanel.activeSelf;

        if (isCurrentlyActive)
        {
            UIManager.Instance.Hide(pausePanel);
            OnResume();
        }
        else
        {
            UIManager.Instance.Show(pausePanel);
            OnPause();
        }
    }

    public void ToggleShopPanel()
    {
        if (pausePanel.activeSelf) return;

        bool isCurrentlyActive = storePanel.activeSelf;

        if (isCurrentlyActive)
        {
            UIManager.Instance.Hide(storePanel);
            OnCloseShop();
        }
        else
        {
            UIManager.Instance.Show(storePanel);
            OnOpenShop();
        }
    }

    public void ResumeGame()
    {
        TogglePausePanel();
    }

    public void RestartGame()
    {
        GameManager.Instance.RestartGame();
        UIManager.Instance.Hide(pausePanel);
    }

    public void QuitGame()
    {
        GameManager.Instance.QuitGame();
    }

    private void OnPause()
    {
        Time.timeScale = 0;
    }

    private void OnResume()
    {
        Time.timeScale = 1;
    }

    private void OnOpenShop()
    {
        Time.timeScale = 0;
    }

    private void OnCloseShop()
    {
        Time.timeScale = 1;
    }
}