using System;
using UnityEngine;

public class CoinModel
{
    public float Money { get; private set; }
    public event Action OnAddMoney;
    public event Action OnRemoveMoney;
    public event Action<float> OnChangeMoney;

    private const string BANK_MONEY = "BANK_MONEY";

    public void Initialize()
    {
        Money = PlayerPrefs.GetFloat(BANK_MONEY, 300);
    }

    public void Destroy()
    {
        PlayerPrefs.SetFloat(BANK_MONEY, Money);
    }

    public void SendMoney(float money)
    {
        if(money >= 0)
        {
            OnAddMoney?.Invoke();
        }
        else
        {
            OnRemoveMoney?.Invoke();
        }
        Money += money;
        OnChangeMoney?.Invoke(Money);

        Debug.Log(Money);
    }

    public bool CanAfford(int bet)
    {
        return Money >= bet;
    }
}
