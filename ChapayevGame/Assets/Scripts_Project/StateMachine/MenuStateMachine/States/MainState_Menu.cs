using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainState_Menu : IState
{
    private UIMainMenuRoot sceneRoot;

    private IGlobalStateMachine stateMachine;

    public MainState_Menu(IGlobalStateMachine stateMachine, UIMainMenuRoot sceneRoot)
    {
        this.stateMachine = stateMachine;
        this.sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        sceneRoot.OnClickToOpenBuyStrategy += ChangeStateToBuyStrategy;
        sceneRoot.OnClickToOpenBuyChip += ChangeStateToBuyChip;
        sceneRoot.OnClickToOpenChooseStrategy += ChangeStateToChooseStrategy;

        sceneRoot.OpenMainPanel();
    }

    public void ExitState()
    {
        sceneRoot.OnClickToOpenBuyStrategy -= ChangeStateToBuyStrategy;
        sceneRoot.OnClickToOpenBuyChip -= ChangeStateToBuyChip;
        sceneRoot.OnClickToOpenChooseStrategy -= ChangeStateToChooseStrategy;
    }

    private void ChangeStateToBuyStrategy()
    {
        stateMachine.SetState(stateMachine.GetState<BuyStrategy_Menu>());
    }

    private void ChangeStateToBuyChip()
    {
        stateMachine.SetState(stateMachine.GetState<BuyChip_Menu>());
    }

    private void ChangeStateToChooseStrategy()
    {
        stateMachine.SetState(stateMachine.GetState<ChooseStrategy_Menu>());
    }
}
