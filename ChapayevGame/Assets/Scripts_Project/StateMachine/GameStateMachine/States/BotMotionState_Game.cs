using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMotionState_Game : IState
{
    private readonly IGlobalStateMachine stateMachine;
    
    public BotMotionState_Game(IGlobalStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public void EnterState()
    {

    }

    public void ExitState()
    {

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
