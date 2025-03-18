using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadBuyStrategy_Game : IState
{
    private readonly UIMiniGameSceneRoot sceneRoot;
    private readonly IGlobalStateMachine stateMachine;
    private readonly IAnimationFrameProvider animationFrameProvider;
    private readonly ISoundProvider soundProvider;

    private IEnumerator coroutineTimer;

    public LoadBuyStrategy_Game(IGlobalStateMachine stateMachine, UIMiniGameSceneRoot sceneRoot, IAnimationFrameProvider animationFrameProvider, ISoundProvider soundProvider)
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

        if (coroutineTimer != null)
            Coroutines.Stop(coroutineTimer);

        coroutineTimer = Timer(3);
        Coroutines.Start(coroutineTimer);

    }

    public void ExitState()
    {
        sceneRoot.CloseLoadBuyStrategyPanel();
        animationFrameProvider.DeactivateAnimation("LoadBuyStrategy");
    }

    private IEnumerator Timer(int cycleCount)
    {
        for (int i = 0; i < cycleCount; i++)
        {
            soundProvider.PlayOneShot("Swoosh");

            yield return new WaitForSeconds(0.75f);
        }

        ChangeStateToChipPresentation();
    }

    private void ChangeStateToChipPresentation()
    {
        stateMachine.SetState(stateMachine.GetState<StrategyPresentation_Game>());
    }
}
