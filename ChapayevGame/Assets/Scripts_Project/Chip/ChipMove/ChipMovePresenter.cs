using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipMovePresenter
{
    private ChipMoveModel model;
    private ChipMoveView view;

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

    }

    private void DeactivateEvents()
    {

    }
}
