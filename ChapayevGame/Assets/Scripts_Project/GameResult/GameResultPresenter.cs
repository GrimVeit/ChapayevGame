using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResultPresenter
{
    private readonly GameResultModel model;
    private readonly GameResultView view;

    public GameResultPresenter(GameResultModel model, GameResultView view)
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

    public void AddPlayerChip(ChipMove chip)
    {

    }

    public void AddBotChip(ChipMove chip)
    {

    }

    public void RemovePlayerChip(ChipMove chip)
    {

    }

    public void RemoveBotChip(ChipMove chip)
    {

    }

    #endregion
}
