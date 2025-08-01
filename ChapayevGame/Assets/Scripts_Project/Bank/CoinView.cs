using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinView : View
{
    [SerializeField] private List<BankDisplay> bankDisplayViews = new List<BankDisplay>();

    public void Initialize()
    {
        for (int i = 0; i < bankDisplayViews.Count; i++)
        {
            bankDisplayViews[i].Initialize();
        }
    }

    public void AddMoney()
    {
        for (int i = 0; i < bankDisplayViews.Count; i++)
        {
            bankDisplayViews[i].AddMoney();
        }
    }

    public void RemoveMoney()
    {
        for (int i = 0; i < bankDisplayViews.Count; i++)
        {
            bankDisplayViews[i].RemoveMoney();
        }
    }

    public void SendMoneyDisplay(float money)
    {
        for (int i = 0; i < bankDisplayViews.Count; i++)
        {
            bankDisplayViews[i].SendMoneyDisplay(money);
        }
    }
}
