using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FromPlayerMotionToBotMotion_Game : IState
{
    private IGlobalStateMachine stateMachine;
    private GameResultPresenter gameResultPresenter;
    private ChipMovePresenter chipMovePresenter;

    public FromPlayerMotionToBotMotion_Game(IGlobalStateMachine stateMachine, GameResultPresenter gameResultPresenter, ChipMovePresenter chipMovePresenter)
    {
        this.stateMachine = stateMachine;
        this.gameResultPresenter = gameResultPresenter;
        this.chipMovePresenter = chipMovePresenter;
    }

    public void EnterState()
    {
        Debug.Log("ACTIVATE STATE - FROM PLAYER TO BOT");

        gameResultPresenter.OnWin += ChangeStateToStartWin;
        gameResultPresenter.OnLose += ChangeStateToStartLose;

        chipMovePresenter.OnDestroyedCurrentChip += ChangeStateToBot;
        chipMovePresenter.OnStoppedCurrentChip += ChangeStateToBot;
    }

    public void ExitState()
    {
        Debug.Log("DEACTIVATE STATE - FROM PLAYER TO BOT");

        gameResultPresenter.OnWin -= ChangeStateToStartWin;
        gameResultPresenter.OnLose -= ChangeStateToStartLose;

        chipMovePresenter.OnDestroyedCurrentChip -= ChangeStateToBot;
        chipMovePresenter.OnStoppedCurrentChip -= ChangeStateToBot;
    }

    private void ChangeStateToStartWin()
    {
        stateMachine.SetState(stateMachine.GetState<StartWinState_Game>());
    }

    private void ChangeStateToStartLose()
    {
        stateMachine.SetState(stateMachine.GetState<StartLoseState_Game>());
    }

    private void ChangeStateToBot()
    {
        stateMachine.SetState(stateMachine.GetState<BotMotionState_Game>());
    }
}
