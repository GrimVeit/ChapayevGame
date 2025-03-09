using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinStartState_Game : IState
{
    private UIMiniGameSceneRoot sceneRoot;
    private readonly IGlobalStateMachine stateMachine;

    private IEnumerator coroutineTimer;

    public SpinStartState_Game(IGlobalStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public void EnterState()
    {
        if(coroutineTimer == null)
            Coroutines.Stop(coroutineTimer);

        coroutineTimer = CoroutineTimer();
        Coroutines.Start(coroutineTimer);

        sceneRoot.OpenMainPanel();
        sceneRoot.OpenSpinStartPanel();
        sceneRoot.OpenSpinPanel();
    }

    public void ExitState()
    {
        if (coroutineTimer == null)
            Coroutines.Stop(coroutineTimer);

        sceneRoot.CloseSpinStartPanel();
    }

    private IEnumerator CoroutineTimer()
    {
        yield return new WaitForSeconds(2);

        ChangeStateToSpin();
    }

    private void ChangeStateToSpin()
    {
        stateMachine.SetState(stateMachine.GetState<SpinState_Game>());
    }
}
