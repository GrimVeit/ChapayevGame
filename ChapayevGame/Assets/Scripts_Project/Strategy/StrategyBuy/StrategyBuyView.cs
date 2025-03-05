using System;
using UnityEngine;
using UnityEngine.UI;

public class StrategyBuyView : View
{
    [SerializeField] private Button buttonBuyStrategy;

    public void Initialize()
    {
        buttonBuyStrategy.onClick.AddListener(()=> OnClickToBuy?.Invoke());
    }

    public void Dispose()
    {
        buttonBuyStrategy.onClick.RemoveListener(() => OnClickToBuy?.Invoke());
    }

    #region Input

    public event Action OnClickToBuy;

    #endregion
}
