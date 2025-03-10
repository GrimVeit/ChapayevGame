using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipPunchPresenter
{
    private readonly ChipPunchModel model;
    private readonly ChipPunchView view;

    public ChipPunchPresenter(ChipPunchModel model, ChipPunchView view)
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

    public void AddPunchChip(Transform transformFirstChip, Transform transformSecondChip, float force)
    {

    }

    #endregion
}
