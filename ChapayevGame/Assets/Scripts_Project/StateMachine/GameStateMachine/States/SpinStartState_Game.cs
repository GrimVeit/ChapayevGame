using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinStartState_Game : IState
{
    private UIMiniGameSceneRoot sceneRoot;
    private readonly IGlobalStateMachine stateMachine;
    private ChipMovePresenter chipMovePresenter;
    private IEnumerator coroutineTimer;

    public SpinStartState_Game(IGlobalStateMachine stateMachine, UIMiniGameSceneRoot sceneRoot, ChipMovePresenter chipMovePresenter)
    {
        this.stateMachine = stateMachine;
        this.sceneRoot = sceneRoot;
        this.chipMovePresenter = chipMovePresenter;
    }

    public void EnterState()
    {
        Debug.Log("ACTIVATE STATE - SPIN START");

        if(coroutineTimer != null)
            Coroutines.Stop(coroutineTimer);

        coroutineTimer = CoroutineTimer();
        Coroutines.Start(coroutineTimer);

        sceneRoot.OpenMainPanel();
        sceneRoot.OpenSpinStartPanel();
        sceneRoot.OpenSpinPanel();

        chipMovePresenter.DeactivateChips();
    }

    public void ExitState()
    {
        Debug.Log("DEACTIVATE STATE - SPIN START");

        if (coroutineTimer != null)
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
