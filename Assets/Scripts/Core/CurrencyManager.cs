using System.Numerics;
using UnityEngine;

public class CurrencyManager : Singleton<CurrencyManager>
{
    private BigInteger coinBalance;
    private BigInteger uniqueCoinBalance;

    private void Start()
    {
        coinBalance = 0;
        uniqueCoinBalance = 0;
    }

    public void AddCoins(BigInteger amount)
    {
        coinBalance += amount;
    }

    public bool SpendCoins(BigInteger amount)
    {
        if (coinBalance >= amount)
        {
            coinBalance -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddUniqueCoins(BigInteger amount)
    {
        uniqueCoinBalance += amount;
    }

    public bool SpendUniqueCoins(BigInteger amount)
    {
        if (uniqueCoinBalance >= amount)
        {
            uniqueCoinBalance -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }

    public BigInteger GetCoinBalance()
    {
        return coinBalance;
    }

    public BigInteger GetUniqueCoinBalance()
    {
        return uniqueCoinBalance;
    }
}
