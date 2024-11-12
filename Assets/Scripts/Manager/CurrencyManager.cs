using System.Numerics;
using UnityEngine;

public class CurrencyManager : Singleton<CurrencyManager>
{
    private BigInteger coinBalance;
    private BigInteger uniqueCoinBalance;

    private void Start()
    {
        coinBalance = 100000000;
        uniqueCoinBalance = 100000000;
    }

    public void AddCoins(BigInteger amount)
    {
        coinBalance += amount;
        Debug.Log("코인 추가됨: " + amount + ", 현재 코인 잔액: " + coinBalance);
    }

    public bool SpendCoins(BigInteger amount)
    {
        if (coinBalance >= amount)
        {
            coinBalance -= amount;
            Debug.Log("코인 사용됨: " + amount + ", 남은 코인 잔액: " + coinBalance);
            return true;
        }
        else
        {
            Debug.Log("코인이 부족합니다. 현재 잔액: " + coinBalance);
            return false;
        }
    }

    public void AddUniqueCoins(BigInteger amount)
    {
        uniqueCoinBalance += amount;
        Debug.Log("유니크 조각 추가됨: " + amount + ", 현재 유니크 조각 잔액: " + uniqueCoinBalance);
    }

    // 유니크 조각 감소 메서드
    public bool SpendUniqueCoins(BigInteger amount)
    {
        if (uniqueCoinBalance >= amount)
        {
            uniqueCoinBalance -= amount;
            Debug.Log("유니크 조각 사용됨: " + amount + ", 남은 유니크 조각 잔액: " + uniqueCoinBalance);
            return true;
        }
        else
        {
            Debug.Log("유니크 조각이 부족합니다. 현재 잔액: " + uniqueCoinBalance);
            return false;
        }
    }

    // 현재 코인 잔액 조회
    public BigInteger GetCoinBalance()
    {
        return coinBalance;
    }

    // 현재 유니크 조각 잔액 조회
    public BigInteger GetUniqueCoinBalance()
    {
        return uniqueCoinBalance;
    }
}
