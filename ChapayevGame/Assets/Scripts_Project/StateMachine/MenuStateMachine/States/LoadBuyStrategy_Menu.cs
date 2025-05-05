using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadBuyStrategy_Menu : IState
{
    private readonly UIMenuRoot sceneRoot;
    private readonly IGlobalStateMachine stateMachine;
    private readonly IAnimationFrameProvider animationFrameProvider;
    private readonly ISoundProvider soundProvider;

    private IEnumerator coroutineTimer;

    public LoadBuyStrategy_Menu(IGlobalStateMachine stateMachine, UIMenuRoot sceneRoot, IAnimationFrameProvider animationFrameProvider, ISoundProvider soundProvider)
    {
        this.stateMachine = stateMachine;
        this.sceneRoot = sceneRoot;
        this.animationFrameProvider = animationFrameProvider;
        this.soundProvider = soundProvider;
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

    private IEnumerator Timer(float cycleCount)
    {
        for(int i = 0; i < cycleCount; i++)
        {
            soundProvider.PlayOneShot("Swoosh");

            yield return new WaitForSeconds(0.75f);
        }

        ChangeStateToStrategyPresentation();
    }

    private void ChangeStateToStrategyPresentation()
    {
        stateMachine.SetState(stateMachine.GetState<StrategyPresentation_Menu>());
    }
}
