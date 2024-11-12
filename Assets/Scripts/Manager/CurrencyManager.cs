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
        Debug.Log("���� �߰���: " + amount + ", ���� ���� �ܾ�: " + coinBalance);
    }

    public bool SpendCoins(BigInteger amount)
    {
        if (coinBalance >= amount)
        {
            coinBalance -= amount;
            Debug.Log("���� ����: " + amount + ", ���� ���� �ܾ�: " + coinBalance);
            return true;
        }
        else
        {
            Debug.Log("������ �����մϴ�. ���� �ܾ�: " + coinBalance);
            return false;
        }
    }

    public void AddUniqueCoins(BigInteger amount)
    {
        uniqueCoinBalance += amount;
        Debug.Log("����ũ ���� �߰���: " + amount + ", ���� ����ũ ���� �ܾ�: " + uniqueCoinBalance);
    }

    // ����ũ ���� ���� �޼���
    public bool SpendUniqueCoins(BigInteger amount)
    {
        if (uniqueCoinBalance >= amount)
        {
            uniqueCoinBalance -= amount;
            Debug.Log("����ũ ���� ����: " + amount + ", ���� ����ũ ���� �ܾ�: " + uniqueCoinBalance);
            return true;
        }
        else
        {
            Debug.Log("����ũ ������ �����մϴ�. ���� �ܾ�: " + uniqueCoinBalance);
            return false;
        }
    }

    // ���� ���� �ܾ� ��ȸ
    public BigInteger GetCoinBalance()
    {
        return coinBalance;
    }

    // ���� ����ũ ���� �ܾ� ��ȸ
    public BigInteger GetUniqueCoinBalance()
    {
        return uniqueCoinBalance;
    }
}
