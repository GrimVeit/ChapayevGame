using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinState_Game : IState
{
    private UIMiniGameSceneRoot sceneRoot;

    private readonly IGlobalStateMachine stateMachine;
    private SpinMotionPresenter spinMotionPresenter;

    public SpinState_Game(IGlobalStateMachine stateMachine, SpinMotionPresenter motionPresenter)
    {
        this.stateMachine = stateMachine;
        this.spinMotionPresenter = motionPresenter;
    }

    public void EnterState()
    {
        spinMotionPresenter.OnBotMotion += ChangeStateToBotMotionState;
        spinMotionPresenter.OnPlayerMotion += ChangeStateToPlayerMotionState;

        spinMotionPresenter.ActivateSpin();
    }

    public void ExitState()
    {
        spinMotionPresenter.OnBotMotion -= ChangeStateToBotMotionState;
        spinMotionPresenter.OnPlayerMotion -= ChangeStateToPlayerMotionState;

        sceneRoot.CloseSpinPanel();
    }

    private void ChangeStateToBotMotionState()
    {
        stateMachine.SetState(stateMachine.GetState<BotMotionState_Game>());
    }

    private void ChangeStateToPlayerMotionState()
    {
        stateMachine.SetState(stateMachine.GetState<PlayerMotionState_Game>());
    }
}
