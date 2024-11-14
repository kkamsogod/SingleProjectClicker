using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Numerics;

public class AttackSpeedBuyPanelController : MonoBehaviour
{
    [SerializeField] private Button buyButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private string itemPriceString = "10000";
    [SerializeField] private float attackSpeedIncrease = 0.2f;
    [SerializeField] private int maxClickCount = 50;
    [SerializeField] private TextMeshProUGUI upgradeInfoText;

    private CharacterStatHandler statHandler;
    private int currentClickCount = 0;
    private BigInteger itemPrice;
    private bool isAnimating = false;

    private void Start()
    {
        itemPrice = BigInteger.Parse(itemPriceString);
        buyButton.onClick.AddListener(AttemptPurchase);
        cancelButton.onClick.AddListener(ClosePanel);

        statHandler = GameManager.Instance.player.GetComponent<CharacterStatHandler>();
        UpdateUI();
        UpdateBuyButtonStatus();
    }

    public void OpenPanel()
    {
        UIManager.Instance.Show(this.gameObject);
    }

    public void ClosePanel()
    {
        UIManager.Instance.Hide(this.gameObject);
    }

    private void AttemptPurchase()
    {
        if (isAnimating) return;

        if (currentClickCount < maxClickCount)
        {
            if (CurrencyManager.Instance.SpendCoins(itemPrice))
            {
                statHandler.UpgradeAttackSpeed(attackSpeedIncrease);
                itemPrice *= 2;

                currentClickCount++;
                UpdateUI();
                UpdateBuyButtonStatus();
            }
            else
            {
                StartCoroutine(PlayInsufficientFundsAnimation());
            }
        }

        if (currentClickCount >= maxClickCount)
        {
            UpdateUI();
            UpdateBuyButtonStatus();
        }
    }

    private IEnumerator PlayInsufficientFundsAnimation()
    {
        isAnimating = true;

        Color originalColor = buyButton.image.color;
        UnityEngine.Vector3 originalPosition = buyButton.transform.position;
        string originalText = upgradeInfoText.text;

        buyButton.image.color = Color.red;
        upgradeInfoText.text = "코인이\n부족합니다.";

        float shakeDuration = 0.5f;
        float shakeAmount = 5f;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            buyButton.transform.position = originalPosition + UnityEngine.Vector3.right * Mathf.Sin(elapsed * 50) * shakeAmount;
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        buyButton.transform.position = originalPosition;
        buyButton.image.color = originalColor;
        upgradeInfoText.text = originalText;

        isAnimating = false;
    }

    private void UpdateUI()
    {
        if (currentClickCount >= maxClickCount)
        {
            upgradeInfoText.text = "최대 공속에 도달했습니다.";
        }
        else
        {
            upgradeInfoText.text = $"공속 {currentClickCount + 1}단계\n{FormatCurrency(itemPrice)} 코인";
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

    private void UpdateBuyButtonStatus()
    {
        buyButton.interactable = currentClickCount < maxClickCount;
    }
}