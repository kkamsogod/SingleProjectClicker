using UnityEngine;
using TMPro;
using System.Numerics;

public class CurrencyUIUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;           // ���� ǥ�� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI uniqueCoinText;     // ����ũ ���� ǥ�� �ؽ�Ʈ

    private void Start()
    {
        UpdateCurrencyUI(); // �ʱ� UI ������Ʈ
    }

    private void Update()
    {
        UpdateCurrencyUI(); // �� ������ UI ������Ʈ
    }

    private void UpdateCurrencyUI()
    {
        if (CurrencyManager.Instance != null)
        {
            // ���ΰ� ����ũ ���� �ܾ��� �������Ͽ� �ؽ�Ʈ ������Ʈ
            coinText.text = FormatCurrency(CurrencyManager.Instance.GetCoinBalance());
            uniqueCoinText.text = FormatCurrency(CurrencyManager.Instance.GetUniqueCoinBalance());
        }
    }

    private string FormatCurrency(BigInteger amount)
    {
        if (amount < 1000)
            return amount.ToString(); // 1,000 �̸��� �״�� ǥ��

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
}