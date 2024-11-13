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
            string formattedCost = FormatNumber(currentUpgrade.upgradeCost);
            upgradeInfoText.text = $"{currentUpgrade.level} �ܰ�\n{formattedCost} ����";
        }
        else
        {
            upgradeInfoText.text = "�ִ� �ܰ�";
            purchaseButton.interactable = false;
        }
    }

    private string FormatNumber(int number)
    {
        if (number >= 100000000)
            return $"{number / 100000000}��";
        else if (number >= 10000)
            return $"{number / 10000}��";
        else
            return number.ToString();
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
                Debug.Log("���� �Ϸ�! ���� ��: " + CurrencyManager.Instance.GetCoinBalance());
            }
            else
            {
                Debug.Log("���� �����մϴ�.");
            }
        }
    }
}
