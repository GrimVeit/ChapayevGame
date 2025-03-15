using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadBuyChip_Game : IState
{
    private readonly UIMiniGameSceneRoot sceneRoot;
    private readonly IGlobalStateMachine stateMachine;
    private readonly IAnimationFrameProvider animationFrameProvider;

    private IEnumerator coroutineTimer;

    public LoadBuyChip_Game(IGlobalStateMachine stateMachine, UIMiniGameSceneRoot sceneRoot, IAnimationFrameProvider animationFrameProvider)
    {
        this.stateMachine = stateMachine;
        this.sceneRoot = sceneRoot;
        this.animationFrameProvider = animationFrameProvider;
    }

    public void EnterState()
    {
        sceneRoot.OpenLoadBuyChipPanel();
        animationFrameProvider.ActivateAnimation("LoadBuyChip", -1);

        if (coroutineTimer != null)
            Coroutines.Stop(coroutineTimer);

        coroutineTimer = Timer(3);
        Coroutines.Start(coroutineTimer);

    }

    public void ExitState()
    {
        sceneRoot.CloseLoadBuyChipPanel();
        animationFrameProvider.DeactivateAnimation("LoadBuyChip");
    }

    private IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);

        ChangeStateToChipPresentation();
    }

    private void ChangeStateToChipPresentation()
    {
        stateMachine.SetState(stateMachine.GetState<ChipPresentation_Game>());
    }
}
