using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProjectileBuyPanelContorller : MonoBehaviour
{
    [SerializeField] private List<RangedAttackSO> upgradeItems;
    [SerializeField] private TextMeshProUGUI upgradeInfoText;
    [SerializeField] private Button purchaseButton;
    [SerializeField] private Button cancelButton;

    private int currentUpgradeIndex = 1;
    private ProjectileAttack playerProjectileAttack;

    private void Start()
    {
        playerProjectileAttack = GameManager.Instance.player.projectileAttack;

        purchaseButton.onClick.AddListener(PurchaseUpgrade);
        cancelButton.onClick.AddListener(ClosePanel);

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (currentUpgradeIndex < upgradeItems.Count)
        {
            RangedAttackSO currentUpgrade = upgradeItems[currentUpgradeIndex];
            upgradeInfoText.text = $"{currentUpgrade.level} 단계\n{currentUpgrade.upgradeCost} 코인";
        }
        else
        {
            upgradeInfoText.text = "최대 단계";
            purchaseButton.interactable = false;
        }
    }

    public void OpenPanel()
    {
        UIManager.Instance.Show(this.gameObject);
    }

    public void ClosePanel()
    {
        UIManager.Instance.Hide(this.gameObject);
    }

    private void PurchaseUpgrade()
    {
        if (currentUpgradeIndex < upgradeItems.Count)
        {
            RangedAttackSO nextUpgrade = upgradeItems[currentUpgradeIndex];

            if (CurrencyManager.Instance.SpendCoins(nextUpgrade.upgradeCost))
            {
                if (playerProjectileAttack != null)
                {
                    playerProjectileAttack.UpgradeAttack(nextUpgrade);
                }

                currentUpgradeIndex++;
                UpdateUI();
                Debug.Log("구매 완료! 남은 돈: " + CurrencyManager.Instance.GetCoinBalance());
            }
            else
            {
                Debug.Log("돈이 부족합니다.");
            }
        }
    }
}
