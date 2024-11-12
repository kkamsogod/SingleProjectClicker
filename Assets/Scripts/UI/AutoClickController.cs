using UnityEngine;
using UnityEngine.UI;

public class AutoClickController : MonoBehaviour
{
    [SerializeField] private Button autoClickButton;
    [SerializeField] private GameObject purchasePanel;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private int autoClickPrice = 100;
    [SerializeField] private float clickInterval = 0.5f;

    public ProjectileAttack projectileAttack;
    public PlayerAnimationController animationController;

    private bool isAutoClickPurchased = false;
    private bool isAutoClickActive = false;
    private float clickTimer = 0f;
    private Image buttonImage;

    private void Start()
    {
        projectileAttack = GameManager.Instance.player.projectileAttack;
        animationController = GameManager.Instance.player.playerAnimationController;

        buttonImage = autoClickButton.GetComponent<Image>();
        autoClickButton.onClick.AddListener(OnAutoClickButtonPressed);
        buyButton.onClick.AddListener(AttemptPurchase);
        cancelButton.onClick.AddListener(ClosePurchasePanel);

        UpdateButtonAppearance();
        purchasePanel.SetActive(false);
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
            purchasePanel.SetActive(true);
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
                Debug.Log("오토클릭 구매 완료!");
                ClosePurchasePanel();
            }
        }
        else
        {
            Debug.Log("돈이 부족합니다.");
        }
    }

    private void ClosePurchasePanel()
    {
        purchasePanel.SetActive(false);
    }

    private void UpdateButtonAppearance()
    {
        Color color = buttonImage.color;
        color.a = isAutoClickPurchased ? 1f : 0.59f;
        buttonImage.color = color;
    }
}