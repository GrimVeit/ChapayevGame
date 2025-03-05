using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipBuyPresenter
{
    private ChipBuyModel model;
    private ChipBuyView view;

    public ChipBuyPresenter(ChipBuyModel model, ChipBuyView view)
    {
        this.model = model;
        this.view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

        view.Initialize();
    }

    public void Dispose()
    {
        DeactivatEvents();

        view.Dispose();
    }

    private void ActivateEvents()
    {
        view.OnClickToBuy += model.Buy;
    }

    private void DeactivatEvents()
    {
        view.OnClickToBuy -= model.Buy;
    }

    #region Input

    public event Action<int> OnBuyChip
    {
        add => model.OnBuyChip += value;
        remove => model.OnBuyChip -= value;
    }
    #endregion
}
