using UnityEngine;

public class UIPanelManager : MonoBehaviour
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
        Debug.Log("게임이 일시 정지되었습니다.");
    }

    private void OnResume()
    {
        Time.timeScale = 1;
        Debug.Log("게임이 재개되었습니다.");
    }

    private void OnOpenShop()
    {
        Time.timeScale = 0;
        Debug.Log("상점이 열렸습니다.");
    }

    private void OnCloseShop()
    {
        Time.timeScale = 1;
        Debug.Log("상점이 닫혔습니다.");
    }
}
