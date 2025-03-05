using System;

public class StrategyBuyPresenter
{
    private StrategyBuyModel model;
    private StrategyBuyView view;

    public StrategyBuyPresenter(StrategyBuyModel model, StrategyBuyView view)
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
        DeactivatEvents();

        view.Dispose();
    }

    private void ActivateEvents()
    {
        view.OnClickToBuy += model.Buy;
    }

    private void DeactivatEvents()
    {
        view.OnClickToBuy -= model.Buy;
    }

    #region Input

    public event Action<int> OnBuyStrategy
    {
        add => model.OnBuyStrategy += value;
        remove => model.OnBuyStrategy -= value;
    }
    #endregion
}
