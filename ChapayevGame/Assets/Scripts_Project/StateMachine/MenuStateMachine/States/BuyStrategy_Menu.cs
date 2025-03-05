using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyStrategy_Menu : IState
{
    private UIMainMenuRoot sceneRoot;
    private StrategyBuyPresenter strategyBuyPresenter;
    private StrategyBuyVisualizePresenter strategyBuyVisualizePresenter;
    private StoreStrategyPresenter storeStrategyPresenter;

    private IGlobalStateMachine stateMachine;

    public BuyStrategy_Menu(IGlobalStateMachine stateMachine, UIMainMenuRoot sceneRoot, StrategyBuyPresenter strategyBuyPresenter, StoreStrategyPresenter storeStrategyPresenter, StrategyBuyVisualizePresenter strategyBuyVisualizePresenter)
    {
        this.sceneRoot = sceneRoot;
        this.strategyBuyPresenter = strategyBuyPresenter;
        this.storeStrategyPresenter = storeStrategyPresenter;
        this.stateMachine = stateMachine;
        this.strategyBuyVisualizePresenter = strategyBuyVisualizePresenter;
    }

    public void EnterState()
    {
        sceneRoot.OnClickToBackFromBuyStrategy += ChangeStateToMain;
        strategyBuyPresenter.OnBuyStrategy += storeStrategyPresenter.OpenStrategy;

        sceneRoot.OpenStoreStrategyPanel();
    }

    public void ExitState()
    {
        sceneRoot.OnClickToBackFromBuyStrategy -= ChangeStateToMain;
        strategyBuyPresenter.OnBuyStrategy -= storeStrategyPresenter.OpenStrategy;
    }

    private void ChangeStateToMain()
    {
        stateMachine.SetState(stateMachine.GetState<MainState_Menu>());
    }
}
