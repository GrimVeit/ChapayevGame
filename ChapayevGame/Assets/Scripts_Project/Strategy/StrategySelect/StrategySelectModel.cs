using System;

public class StrategySelectModel
{
    public event Action<Strategy> OnSetOpenStrategy;
    public event Action<int> OnSelectStrategy;
    public event Action<int> OnDeselectStrategy;

    public event Action<int> OnChooseStrategy;

    private ITutorialDescriptionProvider tutorialDescriptionProvider;

    public StrategySelectModel(ITutorialDescriptionProvider tutorialDescriptionProvider)
    {
        this.tutorialDescriptionProvider = tutorialDescriptionProvider;
    }

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
        tutorialDescriptionProvider.LockTutorial("ChooseStrategy");

        OnChooseStrategy?.Invoke(id);
    }
}
