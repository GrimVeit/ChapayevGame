using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipBotMovePresenter
{
    private readonly ChipBotMoveModel model;

    public ChipBotMovePresenter(ChipBotMoveModel model)
    {
        this.model = model;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {

    }

    #region Input

    public event Action OnDoMotion
    {
        add => model.OnDoMotion += value;
        remove => model.OnDoMotion -= value;
    }

    public event Action OnStoppedChip
    {
        add => model.OnStoppedChip += value;
        remove => model.OnStoppedChip -= value;
    }

    public void ActivateMove()
    {
        model.ActivateMove();
    }

    #endregion
}
