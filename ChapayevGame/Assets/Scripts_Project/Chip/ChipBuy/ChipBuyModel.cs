using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipBuyModel
{
    public event Action<int> OnBuyChip;

    private readonly IMoneyProvider moneyProvider;
    private readonly IStoreChipData storeChipData;

    public ChipBuyModel(IMoneyProvider moneyProvider, IStoreChipData storeStrategyData)
    {
        this.moneyProvider = moneyProvider;
        this.storeChipData = storeStrategyData;
    }

    public void Buy()
    {
        if (!storeChipData.IsAvailableChip()) return;

        if (moneyProvider.CanAfford(500))
        {
            var strategy = storeChipData.GetRandomCloseChip();

            if (strategy == null) return;

            moneyProvider.SendMoney(-500);
            OnBuyChip?.Invoke(strategy.ID);
        }
    }
}
