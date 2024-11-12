using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeControlController : MonoBehaviour
{
    [SerializeField] private Button timeControlButton;
    [SerializeField] private GameObject purchasePanel;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private int timeControlPrice = 100;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private float[] speedMultipliers = { 1f, 2f, 4f };

    private int currentSpeedIndex = 0;
    private bool isTimeControlPurchased = false;
    private Image buttonImage;

    private void Start()
    {
        buttonImage = timeControlButton.GetComponent<Image>();
        timeControlButton.onClick.AddListener(OnTimeControlButtonPressed);
        buyButton.onClick.AddListener(AttemptPurchase);
        cancelButton.onClick.AddListener(ClosePurchasePanel);

        UpdateButtonAppearance();
        UpdateSpeedText();
        purchasePanel.SetActive(false);
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
            purchasePanel.SetActive(true);
        }
    }

    private void UpdateSpeedText()
    {
        if (speedText != null)
        {
            speedText.text = "X" + speedMultipliers[currentSpeedIndex].ToString();
        }
    }

    public void AttemptPurchase()
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
    }

    private void ClosePurchasePanel()
    {
        purchasePanel.SetActive(false);
    }

    private void UpdateButtonAppearance()
    {
        Color color = buttonImage.color;
        color.a = isTimeControlPurchased ? 1f : 0.59f;
        buttonImage.color = color;
    }
}