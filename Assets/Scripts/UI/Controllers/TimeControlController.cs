using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TimeControlController : MonoBehaviour
{
    [SerializeField] private Button timeControlButton;
    [SerializeField] private GameObject purchasePanel;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private long timeControlPrice = 10000000000;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI purchaseText;
    [SerializeField] private float[] speedMultipliers = { 1f, 2f, 4f };

    private int currentSpeedIndex = 0;
    private bool isTimeControlPurchased = false;
    private bool isAnimating = false;
    private Image buttonImage;

    private void Start()
    {
        buttonImage = timeControlButton.GetComponent<Image>();
        timeControlButton.onClick.AddListener(OnTimeControlButtonPressed);
        buyButton.onClick.AddListener(AttemptPurchase);
        cancelButton.onClick.AddListener(ClosePurchasePanel);

        UpdateButtonAppearance();
        UpdateSpeedText();
        UpdatePurchaseText();
        UIManager.Instance.Hide(purchasePanel);
    }

    private void Update()
    {
        if (isTimeControlPurchased)
        {
            float scale = Mathf.PingPong(Time.time, 0.2f) + 0.9f;
            timeControlButton.transform.localScale = new Vector3(scale, scale, 1);
        }
    }

    private void OnTimeControlButtonPressed()
    {
        if (isTimeControlPurchased)
        {
            currentSpeedIndex = (currentSpeedIndex + 1) % speedMultipliers.Length;
            Time.timeScale = speedMultipliers[currentSpeedIndex];
            UpdateSpeedText();
        }
        else
        {
            UIManager.Instance.Show(purchasePanel);
            UpdatePurchaseText();
        }
    }

    private void UpdateSpeedText()
    {
        if (speedText != null)
        {
            speedText.text = "X" + speedMultipliers[currentSpeedIndex].ToString();
        }
    }

    private void AttemptPurchase()
    {
        if (CurrencyManager.Instance.GetCoinBalance() >= timeControlPrice)
        {
            if (CurrencyManager.Instance.SpendCoins(timeControlPrice))
            {
                isTimeControlPurchased = true;
                UpdateButtonAppearance();
                ClosePurchasePanel();
            }
        }
        else
        {
            StartCoroutine(PlayInsufficientFundsAnimation());
        }
    }

    private IEnumerator PlayInsufficientFundsAnimation()
    {
        if (isAnimating) yield break;
        isAnimating = true;

        Color originalColor = buyButton.image.color;
        Vector3 originalPosition = buyButton.transform.position;
        string originalText = purchaseText.text;

        buyButton.image.color = Color.red;
        purchaseText.text = "코인이 부족합니다.";

        float shakeDuration = 0.5f;
        float shakeAmount = 5f;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            buyButton.transform.position = originalPosition + Vector3.right * Mathf.Sin(elapsed * 50) * shakeAmount;
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        buyButton.transform.position = originalPosition;
        buyButton.image.color = originalColor;
        purchaseText.text = originalText;

        isAnimating = false;
    }

    private void ClosePurchasePanel()
    {
        UIManager.Instance.Hide(purchasePanel);
    }

    private void UpdateButtonAppearance()
    {
        Color color = buttonImage.color;
        color.a = isTimeControlPurchased ? 1f : 0.59f;
        buttonImage.color = color;
    }

    private void UpdatePurchaseText()
    {
        if (purchaseText != null)
        {
            string formattedPrice = FormatCurrency(timeControlPrice);
            purchaseText.text = $"시간을 조절할 수 있는 기능입니다.\n\n구매 비용은 {formattedPrice} 코인 입니다.\n\n최대 4배까지 늘릴 수 있습니다.\n\n활성화 하시겠습니까? (영구)";
        }
    }

    private string FormatCurrency(long amount)
    {
        if (amount >= 100000000)
            return $"{amount / 100000000f:F1}억";
        else if (amount >= 10000)
            return $"{amount / 10000}만";
        else
            return amount.ToString();
    }
}