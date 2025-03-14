using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadBuyStrategy_Menu : IState
{
    private readonly UIMainMenuRoot sceneRoot;
    private readonly IGlobalStateMachine stateMachine;

    private IEnumerator coroutineTimer;

    public LoadBuyStrategy_Menu(IGlobalStateMachine stateMachine, UIMainMenuRoot sceneRoot)
    {
        this.stateMachine = stateMachine;
        this.sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        sceneRoot.OpenLoadBuyStrategyPanel();

        if(coroutineTimer != null )
            Coroutines.Stop(coroutineTimer);

        coroutineTimer = Timer(3);
        Coroutines.Start(coroutineTimer);

    }

    public void ExitState()
    {
        sceneRoot.CloseLoadBuyStrategyPanel();
    }

    private IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);

        ChangeStateToStrategyPresentation();
    }

    private void ChangeStateToStrategyPresentation()
    {
        stateMachine.SetState(stateMachine.GetState<StrategyPresentation_Menu>());
    }
}
