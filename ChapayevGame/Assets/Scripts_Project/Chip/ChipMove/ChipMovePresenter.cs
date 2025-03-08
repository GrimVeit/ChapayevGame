using System;
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

        view.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        view.Dispose();
    }

    private void ActivateEvents()
    {
        view.OnDoMotion += model.DoMotion;

        model.OnAddChip += view.AddChip;
        model.OnRemoveChip += view.RemoveChip;

        model.OnActivateChips += view.ActivateChips;
        model.OnDeactivateChips += view.DeactivateChips;
    }

    private void DeactivateEvents()
    {
        view.OnDoMotion -= model.DoMotion;

        model.OnAddChip -= view.AddChip;
        model.OnRemoveChip -= view.RemoveChip;

        model.OnActivateChips -= view.ActivateChips;
        model.OnDeactivateChips -= view.DeactivateChips;
    }

    #region Input

    public event Action OnDoMotion
    {
        add => model.OnDoMotion += value;
        remove => model.OnDoMotion -= value;
    }

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
