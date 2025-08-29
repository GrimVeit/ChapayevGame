using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hello_Menu : IState
{
    private UIMenuRoot sceneRoot;

    private IGlobalStateMachine stateMachine;

    private IEnumerator timer;

    public Hello_Menu(IGlobalStateMachine stateMachine, UIMenuRoot sceneRoot)
    {
        this.stateMachine = stateMachine;
        this.sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        if(timer != null) Coroutines.Stop(timer);

        timer = Timer(2);
        Coroutines.Start(timer);

        sceneRoot.OpenHelloPanel();
    }

    public void ExitState()
    {
        if (timer != null) Coroutines.Stop(timer);
    }

    private IEnumerator Timer(float sec)
    {
        yield return new WaitForSeconds(sec);

        ChangeStateToMain();
    }

    private void ChangeStateToMain()
    {
        stateMachine.SetState(stateMachine.GetState<MainState_Menu>());
    }
}
