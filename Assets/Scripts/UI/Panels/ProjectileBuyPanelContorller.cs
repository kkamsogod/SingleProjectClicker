using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Numerics;
using System;

public class ProjectileBuyPanelController : MonoBehaviour
{
    [SerializeField] private List<RangedAttackSO> upgradeItems;
    [SerializeField] private TextMeshProUGUI upgradeInfoText;
    [SerializeField] private Button purchaseButton;
    [SerializeField] private Button cancelButton;

    public event Action<RangedAttackSO> OnSkillPurchased;

    private int currentUpgradeIndex = 1;
    private bool isAnimating = false;

    private void Start()
    {
        purchaseButton.onClick.AddListener(PurchaseUpgrade);
        cancelButton.onClick.AddListener(ClosePanel);
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (currentUpgradeIndex < upgradeItems.Count)
        {
            RangedAttackSO currentUpgrade = upgradeItems[currentUpgradeIndex];
            string formattedCost = FormatCurrency(currentUpgrade.upgradeCost);
            upgradeInfoText.text = $"{currentUpgradeIndex} 단계\n{formattedCost} 코인";
        }
        else
        {
            upgradeInfoText.text = "최대 단계";
            purchaseButton.interactable = false;
        }
    }

    private string FormatCurrency(BigInteger amount)
    {
        if (amount < 1000)
            return amount.ToString();

        string[] units = { "", "K", "M", "B", "T", "A", "B", "C", "D", "E", "F", "G" };
        int unitIndex = 0;
        BigInteger divisor = new BigInteger(1000);

        while (amount >= divisor && unitIndex < units.Length - 1)
        {
            amount /= 1000;
            unitIndex++;
        }

        return string.Format("{0:F1}{1}", (double)amount, units[unitIndex]);
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
        if (isAnimating) return;

        if (currentUpgradeIndex < upgradeItems.Count)
        {
            RangedAttackSO nextUpgrade = upgradeItems[currentUpgradeIndex];

            if (CurrencyManager.Instance.SpendCoins(nextUpgrade.upgradeCost))
            {
                ProjectileManager.Instance.UpgradeProjectile(nextUpgrade);

                OnSkillPurchased?.Invoke(nextUpgrade);

                currentUpgradeIndex++;
                UpdateUI();
            }
            else
            {
                StartCoroutine(PlayInsufficientFundsAnimation());
            }
        }
    }

    private IEnumerator PlayInsufficientFundsAnimation()
    {
        isAnimating = true;

        Color originalColor = purchaseButton.GetComponent<Image>().color;
        UnityEngine.Vector3 originalPosition = purchaseButton.transform.position;
        string originalText = upgradeInfoText.text;

        purchaseButton.GetComponent<Image>().color = Color.red;
        upgradeInfoText.text = "코인이\n부족합니다.";

        float shakeDuration = 0.5f;
        float shakeAmount = 5f;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            purchaseButton.transform.position = originalPosition + UnityEngine.Vector3.right * Mathf.Sin(elapsed * 50) * shakeAmount;
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        purchaseButton.transform.position = originalPosition;
        purchaseButton.GetComponent<Image>().color = originalColor;
        upgradeInfoText.text = originalText;

        isAnimating = false;
    }
}