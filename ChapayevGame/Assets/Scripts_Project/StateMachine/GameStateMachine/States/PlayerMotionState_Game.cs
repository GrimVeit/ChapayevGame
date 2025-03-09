using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotionState_Game : IState
{
    private readonly IGlobalStateMachine stateMachine;

    public PlayerMotionState_Game(IGlobalStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public void EnterState()
    {

    }

    public void ExitState()
    {

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
