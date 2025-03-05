using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseStrategy_Menu : IState
{
    private UIMainMenuRoot sceneRoot;
    private StoreStrategyPresenter storeStrategyPresenter;
    private StrategySelectPresenter strategySelectPresenter;

    private IGlobalStateMachine stateMachine;

    public ChooseStrategy_Menu(IGlobalStateMachine stateMachine, UIMainMenuRoot sceneRoot, StoreStrategyPresenter storeStrategyPresenter, StrategySelectPresenter strategySelectPresenter)
    {
        this.sceneRoot = sceneRoot;
        this.storeStrategyPresenter = storeStrategyPresenter;
        this.strategySelectPresenter = strategySelectPresenter;
        this.stateMachine = stateMachine;
    }

    public void EnterState()
    {
        sceneRoot.OnClickToCancelFromChooseStrategy += ChangeStateToMain;
        sceneRoot.OnClickToOpenChooseChipFromChooseStrategy += ChangeStateToChooseChip;

        strategySelectPresenter.OnChooseStrategy += storeStrategyPresenter.SelectStrategy;

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

    }

    private void ChangeStateToMain()
    {
        stateMachine.SetState(stateMachine.GetState<MainState_Menu>());
    }
}
