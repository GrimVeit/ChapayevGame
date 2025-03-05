using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrategySelectPresenter
{
    private StrategySelectModel model;
    private StrategySelectView view;

    public StrategySelectPresenter(StrategySelectModel model, StrategySelectView view)
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
        view.OnChooseStrategy += model.ChooseStrategy;

        model.OnSetOpenStrategy += model.SetOpenStrategy;
        model.OnSelectStrategy += model.SelectStrategy;
        model.OnDeselectStrategy += model.DeselectStrategy;
    }

    private void DeactivateEvents()
    {
        view.OnChooseStrategy -= model.ChooseStrategy;

        model.OnSetOpenStrategy -= model.SetOpenStrategy;
        model.OnSelectStrategy -= model.SelectStrategy;
        model.OnDeselectStrategy -= model.DeselectStrategy;
    }

    #region Input

    public event Action<int> OnChooseStrategy
    {
        add => model.OnChooseStrategy += value;
        remove => model.OnChooseStrategy -= value;
    }

    public void SelectStrategy(Strategy strategy)
    {
        model.SelectStrategy(strategy.ID);
    }

    public void DeselectStrategy(Strategy strategy)
    {
        model.DeselectStrategy(strategy.ID);
    }

    public void SetOpenStrategy(Strategy strategy)
    {
        model.SetOpenStrategy(strategy);
    }

    #endregion
}
