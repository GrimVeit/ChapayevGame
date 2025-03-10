using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseStrategy_Game : IState
{
    private UIMiniGameSceneRoot sceneRoot;
    private StoreStrategyPresenter storeStrategyPresenter;
    private StoreChipPresenter storeChipPresenter;
    private StrategySelectPresenter strategySelectPresenter;

    private IGlobalStateMachine stateMachine;

    public ChooseStrategy_Game(IGlobalStateMachine stateMachine, UIMiniGameSceneRoot sceneRoot, StoreStrategyPresenter storeStrategyPresenter, StrategySelectPresenter strategySelectPresenter, StoreChipPresenter storeChipPresenter)
    {
        this.sceneRoot = sceneRoot;
        this.storeStrategyPresenter = storeStrategyPresenter;
        this.strategySelectPresenter = strategySelectPresenter;
        this.stateMachine = stateMachine;
        this.storeChipPresenter = storeChipPresenter;
    }

    public void EnterState()
    {
        sceneRoot.OnClickToCancelFromChooseStrategy += ChangeStateToWin;
        sceneRoot.OnClickToOpenChooseChipFromChooseStrategy += ChangeStateToChooseChip;

        strategySelectPresenter.OnChooseStrategy += storeStrategyPresenter.SelectStrategy;

        storeChipPresenter.UnselectAllChips();
        sceneRoot.OpenChooseStrategyPanel();
    }

    public void ExitState()
    {
        sceneRoot.OnClickToCancelFromChooseStrategy -= ChangeStateToWin;
        sceneRoot.OnClickToOpenChooseChipFromChooseStrategy -= ChangeStateToChooseChip;

        strategySelectPresenter.OnChooseStrategy -= storeStrategyPresenter.SelectStrategy;
    }

    private void ChangeStateToChooseChip()
    {
        stateMachine.SetState(stateMachine.GetState<ChooseChip_Game>());
    }

    private void ChangeStateToWin()
    {
        stateMachine.SetState(stateMachine.GetState<WinState_Game>());
    }
}
