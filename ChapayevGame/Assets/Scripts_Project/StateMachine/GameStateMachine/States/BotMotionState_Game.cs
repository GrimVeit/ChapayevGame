using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMotionState_Game : IState
{
    private readonly IGlobalStateMachine stateMachine;
    private GameResultPresenter gameResultPresenter;
    private ChipBotMovePresenter chipBotMovePresenter;
    private GameArrowPresenter gameArrowPresenter;

    private IEnumerator enumeratorTimer;

    public BotMotionState_Game(IGlobalStateMachine stateMachine, GameResultPresenter gameResultPresenter, ChipBotMovePresenter chipBotMovePresenter, GameArrowPresenter gameArrowPresenter)
    {
        this.stateMachine = stateMachine;
        this.gameResultPresenter = gameResultPresenter;
        this.chipBotMovePresenter = chipBotMovePresenter;
        this.gameArrowPresenter = gameArrowPresenter;
    }

    public void EnterState()
    {
        Debug.Log("ACTIVATE STATE - BOT MOTION");

        gameResultPresenter.OnWin += ChangeStateToStartWin;
        gameResultPresenter.OnLose += ChangeStateToStartLose;
        chipBotMovePresenter.OnDoMotion += ChangeStateToTransitionState;

        chipBotMovePresenter.ActivateMove();
        gameArrowPresenter.RotateUp();
    }

    public void ExitState()
    {
        Debug.Log("DEACTIVATE STATE - BOT MOTION");

        gameResultPresenter.OnWin -= ChangeStateToStartWin;
        gameResultPresenter.OnLose -= ChangeStateToStartLose;
        chipBotMovePresenter.OnDoMotion -= ChangeStateToTransitionState;

        if (enumeratorTimer != null)
            Coroutines.Stop(enumeratorTimer);
    }

    public void ChangeStateToTransitionState()
    {
        stateMachine.SetState(stateMachine.GetState<FromBotMotionToPlayerMotion_Game>());
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
