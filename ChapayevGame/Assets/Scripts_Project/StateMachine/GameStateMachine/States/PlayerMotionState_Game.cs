using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotionState_Game : IState
{
    private readonly IGlobalStateMachine stateMachine;
    private ChipMovePresenter chipMovePresenter;
    private GameResultPresenter gameResultPresenter;

    private ITutorialDescriptionProvider tutorialDescriptionProvider;

    public PlayerMotionState_Game(IGlobalStateMachine stateMachine, ChipMovePresenter chipMovePresenter, GameResultPresenter gameResultPresenter, ITutorialDescriptionProvider tutorialDescriptionProvider)
    {
        this.stateMachine = stateMachine;
        this.chipMovePresenter = chipMovePresenter;
        this.gameResultPresenter = gameResultPresenter;
        this.tutorialDescriptionProvider = tutorialDescriptionProvider;
    }

    public void EnterState()
    {
        Debug.Log("ACTIVATE STATE - PLAYER MOTION");

        gameResultPresenter.OnWin += ChangeStateToWin;
        gameResultPresenter.OnLose += ChangeStateToLose;
        chipMovePresenter.OnDoMotion += ChangeStateToTransitionState;

        chipMovePresenter.ActivateChips();
        tutorialDescriptionProvider.ActivateTutorial("StartGrabChip");
    }

    public void ExitState()
    {
        Debug.Log("DEACTIVATE STATE - PLAYER MOTION");

        gameResultPresenter.OnWin -= ChangeStateToWin;
        gameResultPresenter.OnLose -= ChangeStateToLose;
        chipMovePresenter.OnDoMotion -= ChangeStateToTransitionState;

        chipMovePresenter.DeactivateChips();
    }

    public void ChangeStateToTransitionState()
    {
        stateMachine.SetState(stateMachine.GetState<FromPlayerMotionToBotMotion_Game>());
    }

    public void ChangeStateToWin()
    {
        stateMachine.SetState(stateMachine.GetState<WinState_Game>());
    }

    public void ChangeStateToLose()
    {
        stateMachine.SetState(stateMachine.GetState<LoseState_Game>());
    }
}
