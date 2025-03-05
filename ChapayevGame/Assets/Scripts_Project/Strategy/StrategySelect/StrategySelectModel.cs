using System;

public class StrategySelectModel
{
    public event Action<Strategy> OnSetOpenStrategy;
    public event Action<int> OnSelectStrategy;
    public event Action<int> OnDeselectStrategy;

    public event Action<int> OnChooseStrategy;

    public void SetOpenStrategy(Strategy strategy)
    {
        OnSetOpenStrategy?.Invoke(strategy);
    }

    public void SelectStrategy(int id)
    {
        OnSelectStrategy?.Invoke(id);
    }

    public void DeselectStrategy(int id)
    {
        OnDeselectStrategy?.Invoke(id);
    }



    public void ChooseStrategy(int id)
    {
        OnChooseStrategy?.Invoke(id);
    }
}
