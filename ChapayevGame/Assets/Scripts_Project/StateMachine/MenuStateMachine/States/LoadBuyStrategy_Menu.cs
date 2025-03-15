using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadBuyStrategy_Menu : IState
{
    private readonly UIMainMenuRoot sceneRoot;
    private readonly IGlobalStateMachine stateMachine;
    private readonly IAnimationFrameProvider animationFrameProvider;

    private IEnumerator coroutineTimer;

    public LoadBuyStrategy_Menu(IGlobalStateMachine stateMachine, UIMainMenuRoot sceneRoot, IAnimationFrameProvider animationFrameProvider)
    {
        this.stateMachine = stateMachine;
        this.sceneRoot = sceneRoot;
        this.animationFrameProvider = animationFrameProvider;
    }

    public void EnterState()
    {
        sceneRoot.OpenLoadBuyStrategyPanel();
        animationFrameProvider.ActivateAnimation("LoadBuyStrategy", -1);

        if(coroutineTimer != null )
            Coroutines.Stop(coroutineTimer);

        coroutineTimer = Timer(3);
        Coroutines.Start(coroutineTimer);

    }

    public void ExitState()
    {
        sceneRoot.CloseLoadBuyStrategyPanel();
        animationFrameProvider.DeactivateAnimation("LoadBuyStrategy");
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
