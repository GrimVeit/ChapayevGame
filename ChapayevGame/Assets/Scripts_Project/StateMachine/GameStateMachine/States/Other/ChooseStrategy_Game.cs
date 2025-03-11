using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseStrategy_Game : IState
{
    private UIMiniGameSceneRoot sceneRoot;
    private StoreStrategyPresenter storeStrategyPresenter;
    private StoreChipPresenter storeChipPresenter;
    private StrategySelectPresenter strategySelectPresenter;
    private GameResultPresenter gameResultPresenter;

    private IGlobalStateMachine stateMachine;

    public ChooseStrategy_Game(IGlobalStateMachine stateMachine, UIMiniGameSceneRoot sceneRoot, StoreStrategyPresenter storeStrategyPresenter, StrategySelectPresenter strategySelectPresenter, StoreChipPresenter storeChipPresenter, GameResultPresenter gameResultPresenter)
    {
        this.sceneRoot = sceneRoot;
        this.storeStrategyPresenter = storeStrategyPresenter;
        this.strategySelectPresenter = strategySelectPresenter;
        this.stateMachine = stateMachine;
        this.storeChipPresenter = storeChipPresenter;
        this.gameResultPresenter = gameResultPresenter;
    }

    public void EnterState()
    {
        sceneRoot.OnClickToCancelFromChooseStrategy += CheckGameResult;
        sceneRoot.OnClickToOpenChooseChipFromChooseStrategy += ChangeStateToChooseChip;

        strategySelectPresenter.OnChooseStrategy += storeStrategyPresenter.SelectStrategy;

        storeChipPresenter.UnselectAllChips();
        sceneRoot.OpenChooseStrategyPanel();
    }

    public void ExitState()
    {
        sceneRoot.OnClickToCancelFromChooseStrategy -= CheckGameResult;
        sceneRoot.OnClickToOpenChooseChipFromChooseStrategy -= ChangeStateToChooseChip;

        strategySelectPresenter.OnChooseStrategy -= storeStrategyPresenter.SelectStrategy;
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

    private void ChangeStateToChooseChip()
    {
        stateMachine.SetState(stateMachine.GetState<ChooseChip_Game>());
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
