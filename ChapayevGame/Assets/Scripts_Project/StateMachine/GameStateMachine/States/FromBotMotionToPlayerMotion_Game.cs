using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FromBotMotionToPlayerMotion_Game : IState
{
    private IGlobalStateMachine stateMachine;
    private GameResultPresenter gameResultPresenter;
    private ChipBotMovePresenter chipBotMovePresenter;

    public FromBotMotionToPlayerMotion_Game(IGlobalStateMachine stateMachine, GameResultPresenter gameResultPresenter, ChipBotMovePresenter chipMovePresenter)
    {
        this.stateMachine = stateMachine;
        this.gameResultPresenter = gameResultPresenter;
        this.chipBotMovePresenter = chipMovePresenter;
    }

    public void EnterState()
    {
        Debug.Log("ACTIVATE STATE - FROM BOT TO PLAYER");

        gameResultPresenter.OnWin += ChangeStateToWin;
        gameResultPresenter.OnLose += ChangeStateToLose;
        chipBotMovePresenter.OnStoppedChip += ChangeStateToPlayer;
    }

    public void ExitState()
    {
        Debug.Log("DEACTIVATE STATE - FROM BOT TO PLAYER");

        gameResultPresenter.OnWin -= ChangeStateToWin;
        gameResultPresenter.OnLose -= ChangeStateToLose;
        chipBotMovePresenter.OnStoppedChip -= ChangeStateToPlayer;
    }

    private void ChangeStateToWin()
    {
        stateMachine.SetState(stateMachine.GetState<WinState_Game>());
    }

    private void ChangeStateToLose()
    {
        stateMachine.SetState(stateMachine.GetState<LoseState_Game>());
    }

    private void ChangeStateToPlayer()
    {
        stateMachine.SetState(stateMachine.GetState<PlayerMotionState_Game>());
    }
}
