using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotionState_Game : IState
{
    private readonly IGlobalStateMachine stateMachine;
    private ChipMovePresenter chipMovePresenter;
    private GameResultPresenter gameResultPresenter;

    public PlayerMotionState_Game(IGlobalStateMachine stateMachine, ChipMovePresenter chipMovePresenter, GameResultPresenter gameResultPresenter)
    {
        this.stateMachine = stateMachine;
        this.chipMovePresenter = chipMovePresenter;
        this.gameResultPresenter = gameResultPresenter;
    }

    public void EnterState()
    {
        Debug.Log("ACTIVATE STATE - PLAYER MOTION");

        gameResultPresenter.OnWin += ChangeStateToWin;
        gameResultPresenter.OnLose += ChangeStateToLose;
        chipMovePresenter.OnDoMotion += ChangeStateToBotState;

        chipMovePresenter.ActivateChips();
    }

    public void ExitState()
    {
        Debug.Log("DEACTIVATE STATE - PLAYER MOTION");

        gameResultPresenter.OnWin -= ChangeStateToWin;
        gameResultPresenter.OnLose -= ChangeStateToLose;
        chipMovePresenter.OnDoMotion -= ChangeStateToBotState;

        chipMovePresenter.DeactivateChips();
    }

    public void ChangeStateToBotState()
    {
        stateMachine.SetState(stateMachine.GetState<BotMotionState_Game>());
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
