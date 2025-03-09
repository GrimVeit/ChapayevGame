using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMotionState_Game : IState
{
    private readonly IGlobalStateMachine stateMachine;
    private GameResultPresenter gameResultPresenter;

    private IEnumerator enumeratorTimer;

    public BotMotionState_Game(IGlobalStateMachine stateMachine, GameResultPresenter gameResultPresenter)
    {
        this.stateMachine = stateMachine;
        this.gameResultPresenter = gameResultPresenter;
    }

    public void EnterState()
    {
        Debug.Log("ACTIVATE STATE - BOT MOTION");

        gameResultPresenter.OnWin += ChangeStateToWin;
        gameResultPresenter.OnLose += ChangeStateToLose;

        if(enumeratorTimer != null)
            Coroutines.Stop(enumeratorTimer);

        enumeratorTimer = Timer();
        Coroutines.Start(enumeratorTimer);
    }

    public void ExitState()
    {
        Debug.Log("DEACTIVATE STATE - BOT MOTION");

        gameResultPresenter.OnWin -= ChangeStateToWin;
        gameResultPresenter.OnLose -= ChangeStateToLose;

        if (enumeratorTimer != null)
            Coroutines.Stop(enumeratorTimer);
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(3);

        ChangeStateToPlayerState();
    }

    public void ChangeStateToPlayerState()
    {
        stateMachine.SetState(stateMachine.GetState<PlayerMotionState_Game>());
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
