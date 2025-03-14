using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyStrategy_Menu : IState
{
    private readonly UIMainMenuRoot sceneRoot;
    private readonly StrategyBuyPresenter strategyBuyPresenter;
    private readonly StrategyBuyVisualizePresenter strategyBuyVisualizePresenter;
    private readonly StoreStrategyPresenter storeStrategyPresenter;

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
        strategyBuyPresenter.OnSelectRandom += ChangeStateToLoadBuyStrategy;
        //strategyBuyPresenter.OnBuyStrategy += storeStrategyPresenter.OpenStrategy;

        sceneRoot.OpenStoreStrategyPanel();
    }

    public void ExitState()
    {
        sceneRoot.OnClickToBackFromBuyStrategy -= ChangeStateToMain;
        strategyBuyPresenter.OnSelectRandom -= ChangeStateToLoadBuyStrategy;
        //strategyBuyPresenter.OnBuyStrategy -= storeStrategyPresenter.OpenStrategy;
    }

    private void ChangeStateToMain()
    {
        stateMachine.SetState(stateMachine.GetState<MainState_Menu>());
    }

    private void ChangeStateToLoadBuyStrategy()
    {
        stateMachine.SetState(stateMachine.GetState<LoadBuyStrategy_Menu>());
    }
}
