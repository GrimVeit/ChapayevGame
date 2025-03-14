using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadBuyStrategy_Game : IState
{
    private readonly UIMiniGameSceneRoot sceneRoot;
    private readonly IGlobalStateMachine stateMachine;

    private IEnumerator coroutineTimer;

    public LoadBuyStrategy_Game(IGlobalStateMachine stateMachine, UIMiniGameSceneRoot sceneRoot)
    {
        this.stateMachine = stateMachine;
        this.sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        sceneRoot.OpenLoadBuyStrategyPanel();

        if (coroutineTimer != null)
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

        ChangeStateToChipPresentation();
    }

    private void ChangeStateToChipPresentation()
    {
        stateMachine.SetState(stateMachine.GetState<StrategyPresentation_Game>());
    }
}
