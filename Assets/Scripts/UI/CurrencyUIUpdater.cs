using UnityEngine;
using TMPro;
using System.Numerics;

public class CurrencyUIUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;           // 코인 표시 텍스트
    [SerializeField] private TextMeshProUGUI uniqueCoinText;     // 유니크 코인 표시 텍스트

    private void Start()
    {
        UpdateCurrencyUI(); // 초기 UI 업데이트
    }

    private void Update()
    {
        UpdateCurrencyUI(); // 매 프레임 UI 업데이트
    }

    private void UpdateCurrencyUI()
    {
        if (CurrencyManager.Instance != null)
        {
            // 코인과 유니크 코인 잔액을 포맷팅하여 텍스트 업데이트
            coinText.text = FormatCurrency(CurrencyManager.Instance.GetCoinBalance());
            uniqueCoinText.text = FormatCurrency(CurrencyManager.Instance.GetUniqueCoinBalance());
        }
    }

    private string FormatCurrency(BigInteger amount)
    {
        if (amount < 1000)
            return amount.ToString(); // 1,000 미만은 그대로 표시

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