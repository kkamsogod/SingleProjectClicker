using UnityEngine;
using UnityEngine.UI;

public class AttackSpeedBuyPanelController : MonoBehaviour
{
    [SerializeField] private Button buyButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private int itemPrice = 100;

    private void Start()
    {
        buyButton.onClick.AddListener(AttemptPurchase);
        cancelButton.onClick.AddListener(ClosePanel);
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
        if (CurrencyManager.Instance.SpendCoins(itemPrice))
        {
            Debug.Log("���� �Ϸ�! ���� ��: " + CurrencyManager.Instance.GetCoinBalance());
            ClosePanel();
        }
        else
        {
            Debug.Log("���� �����մϴ�.");
        }
    }
}