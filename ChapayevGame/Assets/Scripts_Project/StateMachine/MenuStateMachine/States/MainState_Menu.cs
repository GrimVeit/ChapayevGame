using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainState_Menu : IState
{
    private UIMainMenuRoot sceneRoot;

    private IGlobalStateMachine stateMachine;

    public MainState_Menu(IGlobalStateMachine stateMachine, UIMainMenuRoot sceneRoot)
    {
        this.stateMachine = stateMachine;
        this.sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        sceneRoot.OpenMainPanel();
    }

    public void ExitState()
    {

    }
}
