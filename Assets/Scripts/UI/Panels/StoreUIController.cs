using UnityEngine;
using UnityEngine.UI;

public class StoreUIController : MonoBehaviour
{
    [Header("AttackSpeed")]
    [SerializeField] private Button attackSpeedUpgradeButton;
    [SerializeField] private GameObject attackSpeedPanel;

    [Header("Projectile")]
    [SerializeField] private Button projectileUpgradeButton;
    [SerializeField] private GameObject projectilePanel;

    private void Awake()
    {
        attackSpeedUpgradeButton.onClick.AddListener(() => TogglePanel(attackSpeedPanel));
        projectileUpgradeButton.onClick.AddListener(() => TogglePanel(projectilePanel));
    }

    private void TogglePanel(GameObject panel)
    {
        CloseAllPanels();
        panel.SetActive(!panel.activeSelf);
    }

    private void CloseAllPanels()
    {
        attackSpeedPanel.SetActive(false);
        projectilePanel.SetActive(false);
    }
}
