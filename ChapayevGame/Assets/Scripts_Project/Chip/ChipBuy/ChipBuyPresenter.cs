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
    }

    public void Dispose()
    {
        DeactivateEvents();
    }

    private void ActivateEvents()
    {

    }

    private void DeactivateEvents()
    {

    }
}
