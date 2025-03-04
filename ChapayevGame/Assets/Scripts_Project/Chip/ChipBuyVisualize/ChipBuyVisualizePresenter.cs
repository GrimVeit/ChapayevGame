using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipBuyVisualizePresenter
{
    private readonly ChipBuyVisualizeModel model;
    private readonly ChipBuyVisualizeView view;

    public ChipBuyVisualizePresenter(ChipBuyVisualizeModel model, ChipBuyVisualizeView view)
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
        DeactivateEvents();

        view.Dispose();
    }

    private void ActivateEvents()
    {
        model.OnSetCloseChip += view.SetCloseChipBuyVisualize;
        model.OnSetOpenChip += view.SetOpenChipBuyVisualize;
    }

    private void DeactivateEvents()
    {
        model.OnSetCloseChip -= view.SetCloseChipBuyVisualize;
        model.OnSetOpenChip -= view.SetOpenChipBuyVisualize;
    }

    #region Input

    public void SetOpenChip(Chip chip)
    {
        model.SetOpenChip(chip);
    }

    public void SetCloseChip(Chip chip)
    {
        model.SetCloseChip(chip);
    }

    #endregion
}
