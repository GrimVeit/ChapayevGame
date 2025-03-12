using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMotionState_Game : IState
{
    private readonly IGlobalStateMachine stateMachine;
    private GameResultPresenter gameResultPresenter;
    private ChipBotMovePresenter chipBotMovePresenter;

    private IEnumerator enumeratorTimer;

    public BotMotionState_Game(IGlobalStateMachine stateMachine, GameResultPresenter gameResultPresenter, ChipBotMovePresenter chipBotMovePresenter)
    {
        this.stateMachine = stateMachine;
        this.gameResultPresenter = gameResultPresenter;
        this.chipBotMovePresenter = chipBotMovePresenter;
    }

    public void EnterState()
    {
        Debug.Log("ACTIVATE STATE - BOT MOTION");

        gameResultPresenter.OnWin += ChangeStateToWin;
        gameResultPresenter.OnLose += ChangeStateToLose;
        chipBotMovePresenter.OnDoMotion += ChangeStateToTransitionState;

        chipBotMovePresenter.ActivateMove();
    }

    public void ExitState()
    {
        Debug.Log("DEACTIVATE STATE - BOT MOTION");

        gameResultPresenter.OnWin -= ChangeStateToWin;
        gameResultPresenter.OnLose -= ChangeStateToLose;
        chipBotMovePresenter.OnDoMotion -= ChangeStateToTransitionState;

        if (enumeratorTimer != null)
            Coroutines.Stop(enumeratorTimer);
    }

    public void ChangeStateToTransitionState()
    {
        stateMachine.SetState(stateMachine.GetState<FromBotMotionToPlayerMotion_Game>());
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
