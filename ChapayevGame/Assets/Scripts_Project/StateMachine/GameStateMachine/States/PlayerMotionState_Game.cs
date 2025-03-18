using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotionState_Game : IState
{
    private readonly IGlobalStateMachine stateMachine;
    private ChipMovePresenter chipMovePresenter;
    private GameResultPresenter gameResultPresenter;
    private GameArrowPresenter gameArrowPresenter;

    private ITutorialDescriptionProvider tutorialDescriptionProvider;

    public PlayerMotionState_Game(IGlobalStateMachine stateMachine, ChipMovePresenter chipMovePresenter, GameResultPresenter gameResultPresenter, GameArrowPresenter gameArrowPresenter, ITutorialDescriptionProvider tutorialDescriptionProvider)
    {
        this.stateMachine = stateMachine;
        this.chipMovePresenter = chipMovePresenter;
        this.gameResultPresenter = gameResultPresenter;
        this.gameArrowPresenter = gameArrowPresenter;
        this.tutorialDescriptionProvider = tutorialDescriptionProvider;
    }

    public void EnterState()
    {
        Debug.Log("ACTIVATE STATE - PLAYER MOTION");

        gameResultPresenter.OnWin += ChangeStateToStartWin;
        gameResultPresenter.OnLose += ChangeStateToStartLose;
        chipMovePresenter.OnDoMotion += ChangeStateToTransitionState;

        chipMovePresenter.ActivateChips();
        tutorialDescriptionProvider.ActivateTutorial("StartGrabChip");
        gameArrowPresenter.RotateDown();
    }

    public void ExitState()
    {
        Debug.Log("DEACTIVATE STATE - PLAYER MOTION");

        gameResultPresenter.OnWin -= ChangeStateToStartWin;
        gameResultPresenter.OnLose -= ChangeStateToStartLose;
        chipMovePresenter.OnDoMotion -= ChangeStateToTransitionState;

        chipMovePresenter.DeactivateChips();
    }

    public void ChangeStateToTransitionState()
    {
        stateMachine.SetState(stateMachine.GetState<FromPlayerMotionToBotMotion_Game>());
    }

    public void ChangeStateToStartWin()
    {
        stateMachine.SetState(stateMachine.GetState<StartWinState_Game>());
    }

    public void ChangeStateToStartLose()
    {
        stateMachine.SetState(stateMachine.GetState<StartLoseState_Game>());
    }
}
