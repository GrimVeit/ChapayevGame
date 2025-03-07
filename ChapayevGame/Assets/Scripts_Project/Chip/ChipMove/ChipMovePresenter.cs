using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipMovePresenter
{
    private readonly ChipMoveModel model;
    private readonly ChipMoveView view;

    public ChipMovePresenter(ChipMoveModel model, ChipMoveView view)
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
        model.OnAddChip += view.AddChip;
        model.OnRemoveChip += view.RemoveChip;
    }

    private void DeactivateEvents()
    {
        model.OnAddChip -= view.AddChip;
        model.OnRemoveChip -= view.RemoveChip;
    }

    #region Input

    public void AddChip(ChipMove chip)
    {
        model.AddChip(chip);
    }

    public void RemoveChip(ChipMove chip)
    {
        model.RemoveChip(chip);
    }

    #endregion
}
