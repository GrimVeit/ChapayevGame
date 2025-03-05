using System;
public class StrategyBuyModel
{
    public event Action<int> OnBuyStrategy;

    private readonly IMoneyProvider moneyProvider;
    private readonly IStoreStrategyData storeStrategyData;

    public StrategyBuyModel(IMoneyProvider moneyProvider, IStoreStrategyData storeStrategyData)
    {
        this.moneyProvider = moneyProvider;
        this.storeStrategyData = storeStrategyData;
    }

    public void Buy()
    {
        if (!storeStrategyData.IsAvailableStrategy()) return;

        if (moneyProvider.CanAfford(500))
        {
            var strategy = storeStrategyData.GetRandomCloseStrategy();

            if(strategy == null) return;

            moneyProvider.SendMoney(-500);
            OnBuyStrategy?.Invoke(strategy.ID);
        }
    }
}
