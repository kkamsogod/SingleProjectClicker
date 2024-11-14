using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AutoClickController : MonoBehaviour
{
    [SerializeField] private Button autoClickButton;
    [SerializeField] private GameObject purchasePanel;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private long autoClickPrice = 1000000000;
    [SerializeField] private float clickInterval = 0.5f;
    [SerializeField] private TextMeshProUGUI purchaseText;

    public ProjectileAttack projectileAttack;
    public PlayerAnimationController animationController;

    private bool isAutoClickPurchased = false;
    private bool isAutoClickActive = false;
    private float clickTimer = 0f;
    private Image buttonImage;
    private bool isAnimating = false;

    private void Start()
    {
        projectileAttack = GameManager.Instance.player.projectileAttack;
        animationController = GameManager.Instance.player.playerAnimationController;

        buttonImage = autoClickButton.GetComponent<Image>();
        autoClickButton.onClick.AddListener(OnAutoClickButtonPressed);
        buyButton.onClick.AddListener(AttemptPurchase);
        cancelButton.onClick.AddListener(ClosePurchasePanel);

        UpdateButtonAppearance();
        UpdatePurchaseText();
        UIManager.Instance.Hide(purchasePanel);
    }

    private void Update()
    {
        if (isAutoClickActive)
        {
            clickTimer += Time.deltaTime;
            if (clickTimer >= clickInterval)
            {
                PerformAutoClick();
                clickTimer = 0f;
            }

            float scale = Mathf.PingPong(Time.time / 4, 0.2f) + 0.9f;
            autoClickButton.transform.localScale = new Vector3(scale, scale, 1);
        }
    }

    private void OnAutoClickButtonPressed()
    {
        if (isAutoClickPurchased)
        {
            isAutoClickActive = !isAutoClickActive;

            if (!isAutoClickActive)
            {
                autoClickButton.transform.localScale = Vector3.one;
            }
        }
        else
        {
            UIManager.Instance.Show(purchasePanel);
            UpdatePurchaseText();
        }
    }

    private void PerformAutoClick()
    {
        projectileAttack?.Fire();
        animationController?.TriggerAttackAnimation();
    }

    private void AttemptPurchase()
    {
        if (CurrencyManager.Instance.GetCoinBalance() >= autoClickPrice)
        {
            if (CurrencyManager.Instance.SpendCoins(autoClickPrice))
            {
                isAutoClickPurchased = true;
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
        color.a = isAutoClickPurchased ? 1f : 0.59f;
        buttonImage.color = color;
    }

    private void UpdatePurchaseText()
    {
        string formattedPrice = FormatCurrency(autoClickPrice);
        purchaseText.text = $"자동으로 클릭하는 기능입니다.\n\n구매 비용은 {formattedPrice} 코인 입니다.\n\n클릭 속도는 변경 불가합니다.\n\n활성화 하시겠습니까? (영구)";
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