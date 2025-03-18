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

        gameResultPresenter.OnWin += ChangeStateToStartWin;
        gameResultPresenter.OnLose += ChangeStateToStartLose;

        chipBotMovePresenter.OnStoppedCurrentChip += ChangeStateToPlayer;
        chipBotMovePresenter.OnDestroyedCurrentChip += ChangeStateToPlayer;
    }

    public void ExitState()
    {
        Debug.Log("DEACTIVATE STATE - FROM BOT TO PLAYER");

        gameResultPresenter.OnWin -= ChangeStateToStartWin;
        gameResultPresenter.OnLose -= ChangeStateToStartLose;

        chipBotMovePresenter.OnStoppedCurrentChip -= ChangeStateToPlayer;
        chipBotMovePresenter.OnDestroyedCurrentChip -= ChangeStateToPlayer;
    }

    private void ChangeStateToStartWin()
    {
        stateMachine.SetState(stateMachine.GetState<StartWinState_Game>());
    }

    private void ChangeStateToStartLose()
    {
        stateMachine.SetState(stateMachine.GetState<StartLoseState_Game>());
    }

    private void ChangeStateToPlayer()
    {
        stateMachine.SetState(stateMachine.GetState<PlayerMotionState_Game>());
    }
}
