using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainState_Menu : IState
{
    private UIMenuRoot sceneRoot;

    private IGlobalStateMachine stateMachine;
    private IAnimationFrameProvider animationProvider;

    public MainState_Menu(IGlobalStateMachine stateMachine, UIMenuRoot sceneRoot, IAnimationFrameProvider animationProvider)
    {
        this.stateMachine = stateMachine;
        this.sceneRoot = sceneRoot;
        this.animationProvider = animationProvider;
    }

    public void EnterState()
    {
        sceneRoot.OnClickToOpenBuyStrategy += ChangeStateToBuyStrategy;
        sceneRoot.OnClickToOpenBuyChip += ChangeStateToBuyChip;
        sceneRoot.OnClickToOpenChooseStrategy += ChangeStateToChooseStrategy;

        sceneRoot.OpenMainPanel();
        animationProvider.ActivateAnimation("MenuLoad", 1);
    }

    public void ExitState()
    {
        sceneRoot.OnClickToOpenBuyStrategy -= ChangeStateToBuyStrategy;
        sceneRoot.OnClickToOpenBuyChip -= ChangeStateToBuyChip;
        sceneRoot.OnClickToOpenChooseStrategy -= ChangeStateToChooseStrategy;

        animationProvider.DeactivateAnimation("MenuLoad");
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
