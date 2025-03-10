using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyStrategy_Game : IState
{
    private UIMiniGameSceneRoot sceneRoot;
    private StrategyBuyPresenter strategyBuyPresenter;
    private StrategyBuyVisualizePresenter strategyBuyVisualizePresenter;
    private StoreStrategyPresenter storeStrategyPresenter;

    private IGlobalStateMachine stateMachine;

    public BuyStrategy_Game(IGlobalStateMachine stateMachine, UIMiniGameSceneRoot sceneRoot, StrategyBuyPresenter strategyBuyPresenter, StoreStrategyPresenter storeStrategyPresenter, StrategyBuyVisualizePresenter strategyBuyVisualizePresenter)
    {
        this.sceneRoot = sceneRoot;
        this.strategyBuyPresenter = strategyBuyPresenter;
        this.storeStrategyPresenter = storeStrategyPresenter;
        this.stateMachine = stateMachine;
        this.strategyBuyVisualizePresenter = strategyBuyVisualizePresenter;
    }

    public void EnterState()
    {
        sceneRoot.OnClickToBackFromBuyStrategy += ChangeStateToWin;
        strategyBuyPresenter.OnBuyStrategy += storeStrategyPresenter.OpenStrategy;

        sceneRoot.OpenStoreStrategyPanel();
    }

    public void ExitState()
    {
        sceneRoot.OnClickToBackFromBuyStrategy -= ChangeStateToWin;
        strategyBuyPresenter.OnBuyStrategy -= storeStrategyPresenter.OpenStrategy;
    }

    private void ChangeStateToWin()
    {
        stateMachine.SetState(stateMachine.GetState<WinState_Game>());
    }

    private void ChangeStateToLose()
    {
        stateMachine.SetState(stateMachine.GetState<LoseState_Game>());
    }
}
