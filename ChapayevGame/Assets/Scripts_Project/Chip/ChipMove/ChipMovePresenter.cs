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

        model.OnActivateChips += view.ActivateChips;
        model.OnDeactivateChips += view.DeactivateChips;
    }

    private void DeactivateEvents()
    {
        model.OnAddChip -= view.AddChip;
        model.OnRemoveChip -= view.RemoveChip;

        model.OnActivateChips -= view.ActivateChips;
        model.OnDeactivateChips -= view.DeactivateChips;
    }

    #region Input

    public void AddChip(ChipMove chip)
    {
        model.AddChip((ChipMove_Player)chip);
    }

    public void RemoveChip(ChipMove chip)
    {
        model.RemoveChip((ChipMove_Player)chip);
    }

    public void ActivateChips()
    {
        model.ActivateChips();
    }

    public void DeactivateChips()
    {
        model.DeactivateChips();
    }

    #endregion
}
