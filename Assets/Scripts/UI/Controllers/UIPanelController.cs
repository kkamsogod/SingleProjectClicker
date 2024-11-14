using UnityEngine;

public class UIPanelController : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject storePanel;

    private bool isPausePanelActive = false;
    private bool isStorePanelActive = false;

    private void Awake()
    {
        pausePanel.SetActive(false);
        storePanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePausePanel();
        }
    }

    public void TogglePausePanel()
    {
        if (isStorePanelActive) return;

        isPausePanelActive = !isPausePanelActive;

        if (isPausePanelActive)
        {
            UIManager.Instance.Show(pausePanel);
            OnPause();
        }
        else
        {
            UIManager.Instance.Hide(pausePanel);
            OnResume();
        }
    }

    public void ToggleShopPanel()
    {
        if (isPausePanelActive) return;

        isStorePanelActive = !isStorePanelActive;

        if (isStorePanelActive)
        {
            UIManager.Instance.Show(storePanel);
            OnOpenShop();
        }
        else
        {
            UIManager.Instance.Hide(storePanel);
            OnCloseShop();
        }
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
