using UnityEngine;
using TMPro;
using System.Numerics;

public class CurrencyDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI uniqueCoinText;

    private void Start()
    {
        UpdateCurrencyUI();
    }

    private void Update()
    {
        UpdateCurrencyUI();
    }

    private void UpdateCurrencyUI()
    {
        if (CurrencyManager.Instance != null)
        {
            coinText.text = FormatCurrency(CurrencyManager.Instance.GetCoinBalance());
            uniqueCoinText.text = FormatCurrency(CurrencyManager.Instance.GetUniqueCoinBalance());
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
}