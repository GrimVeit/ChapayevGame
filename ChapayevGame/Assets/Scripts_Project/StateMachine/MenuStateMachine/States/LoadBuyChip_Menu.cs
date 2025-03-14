using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadBuyChip_Menu : IState
{
    private readonly UIMainMenuRoot sceneRoot;
    private readonly IGlobalStateMachine stateMachine;

    private IEnumerator coroutineTimer;

    public LoadBuyChip_Menu(IGlobalStateMachine stateMachine, UIMainMenuRoot sceneRoot)
    {
        this.stateMachine = stateMachine;
        this.sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        sceneRoot.OpenLoadBuyChipPanel();

        if (coroutineTimer != null)
            Coroutines.Stop(coroutineTimer);

        coroutineTimer = Timer(3);
        Coroutines.Start(coroutineTimer);

    }

    public void ExitState()
    {
        sceneRoot.CloseLoadBuyChipPanel();
    }

    private IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);

        ChangeStateToChipPresentation();
    }

    private void ChangeStateToChipPresentation()
    {
        stateMachine.SetState(stateMachine.GetState<ChipPresentation_Menu>());
    }
}
