using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyStrategy_Game : IState
{
    private UIMiniGameSceneRoot sceneRoot;
    private StrategyBuyPresenter strategyBuyPresenter;
    private StrategyBuyVisualizePresenter strategyBuyVisualizePresenter;
    private StoreStrategyPresenter storeStrategyPresenter;
    private GameResultPresenter gameResultPresenter;

    private IGlobalStateMachine stateMachine;

    public BuyStrategy_Game(IGlobalStateMachine stateMachine, UIMiniGameSceneRoot sceneRoot, StrategyBuyPresenter strategyBuyPresenter, StoreStrategyPresenter storeStrategyPresenter, StrategyBuyVisualizePresenter strategyBuyVisualizePresenter, GameResultPresenter gameResultPresenter)
    {
        this.sceneRoot = sceneRoot;
        this.strategyBuyPresenter = strategyBuyPresenter;
        this.storeStrategyPresenter = storeStrategyPresenter;
        this.stateMachine = stateMachine;
        this.strategyBuyVisualizePresenter = strategyBuyVisualizePresenter;
        this.gameResultPresenter = gameResultPresenter;
    }

    public void EnterState()
    {
        sceneRoot.OnClickToBackFromBuyStrategy += CheckGameResult;
        strategyBuyPresenter.OnSelectRandom += ChangeStateToLoadBuyStrategy;

        sceneRoot.OpenStoreStrategyPanel();
    }

    public void ExitState()
    {
        sceneRoot.OnClickToBackFromBuyStrategy -= CheckGameResult;
        strategyBuyPresenter.OnSelectRandom -= ChangeStateToLoadBuyStrategy;
    }

    private void CheckGameResult()
    {
        if (gameResultPresenter.IsPlayerWin())
        {
            ChangeStateToWin();
        }
        else
        {
            ChangeStateToLose();
        }
    }

    private void ChangeStateToWin()
    {
        stateMachine.SetState(stateMachine.GetState<WinState_Game>());
    }

    private void ChangeStateToLose()
    {
        stateMachine.SetState(stateMachine.GetState<LoseState_Game>());
    }

    private void ChangeStateToLoadBuyStrategy()
    {
        stateMachine.SetState(stateMachine.GetState<LoadBuyStrategy_Game>());
    }
}
