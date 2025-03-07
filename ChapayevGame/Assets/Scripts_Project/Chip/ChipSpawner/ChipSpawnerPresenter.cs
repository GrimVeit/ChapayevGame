using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipSpawnerPresenter
{
    private ChipSpawnerModel model;
    private ChipSpawnerView view;

    public ChipSpawnerPresenter(ChipSpawnerModel model, ChipSpawnerView view)
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

    #region Input

    public void SetStrategy(Strategy strategy)
    {

    }

    #endregion
}
