using System;
using UnityEngine;
using UnityEngine.UI;

public class ChipBuyView : View
{
    [SerializeField] private Button buttonBuyChip;

    public void Initialize()
    {
        buttonBuyChip.onClick.AddListener(() => OnClickToBuy?.Invoke());
    }

    public void Dispose()
    {
        buttonBuyChip.onClick.RemoveListener(() => OnClickToBuy?.Invoke());
    }

    #region Input

    public event Action OnClickToBuy;

    #endregion
}
