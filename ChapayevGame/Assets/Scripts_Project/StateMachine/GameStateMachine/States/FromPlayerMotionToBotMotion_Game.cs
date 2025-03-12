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

        gameResultPresenter.OnWin += ChangeStateToWin;
        gameResultPresenter.OnLose += ChangeStateToLose;
        chipMovePresenter.OnStoppedChip += ChangeStateToBot;
    }

    public void ExitState()
    {
        Debug.Log("DEACTIVATE STATE - FROM PLAYER TO BOT");

        gameResultPresenter.OnWin -= ChangeStateToWin;
        gameResultPresenter.OnLose -= ChangeStateToLose;
        chipMovePresenter.OnStoppedChip -= ChangeStateToBot;
    }

    private void ChangeStateToWin()
    {
        stateMachine.SetState(stateMachine.GetState<WinState_Game>());
    }

    private void ChangeStateToLose()
    {
        stateMachine.SetState(stateMachine.GetState<LoseState_Game>());
    }

    private void ChangeStateToBot()
    {
        Debug.Log("lol");

        stateMachine.SetState(stateMachine.GetState<BotMotionState_Game>());
    }
}
