using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseStrategy_Menu : IState
{
    private UIMainMenuRoot sceneRoot;
    private StoreStrategyPresenter storeStrategyPresenter;
    private StoreChipPresenter storeChipPresenter;
    private StrategySelectPresenter strategySelectPresenter;

    private IGlobalStateMachine stateMachine;

    public ChooseStrategy_Menu(IGlobalStateMachine stateMachine, UIMainMenuRoot sceneRoot, StoreStrategyPresenter storeStrategyPresenter, StrategySelectPresenter strategySelectPresenter, StoreChipPresenter storeChipPresenter)
    {
        this.sceneRoot = sceneRoot;
        this.storeStrategyPresenter = storeStrategyPresenter;
        this.strategySelectPresenter = strategySelectPresenter;
        this.stateMachine = stateMachine;
        this.storeChipPresenter = storeChipPresenter;
    }

    public void EnterState()
    {
        sceneRoot.OnClickToCancelFromChooseStrategy += ChangeStateToMain;
        sceneRoot.OnClickToOpenChooseChipFromChooseStrategy += ChangeStateToChooseChip;

        strategySelectPresenter.OnChooseStrategy += storeStrategyPresenter.SelectStrategy;

        storeChipPresenter.UnselectAllChips();
        sceneRoot.OpenChooseStrategyPanel();
    }

    public void ExitState()
    {
        sceneRoot.OnClickToCancelFromChooseStrategy -= ChangeStateToMain;
        sceneRoot.OnClickToOpenChooseChipFromChooseStrategy -= ChangeStateToChooseChip;

        strategySelectPresenter.OnChooseStrategy -= storeStrategyPresenter.SelectStrategy;
    }

    private void ChangeStateToChooseChip()
    {
        stateMachine.SetState(stateMachine.GetState<ChooseChip_Menu>());
    }

    private void ChangeStateToMain()
    {
        stateMachine.SetState(stateMachine.GetState<MainState_Menu>());
    }
}
