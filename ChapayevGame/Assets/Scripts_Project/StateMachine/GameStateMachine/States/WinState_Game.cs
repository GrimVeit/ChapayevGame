using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState_Game : IState
{
    private UIMiniGameSceneRoot sceneRoot;

    public WinState_Game(UIMiniGameSceneRoot sceneRoot)
    {
        this.sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        Debug.Log("ACTIVATE STATE - WIN");

        sceneRoot.OpenWinPanel();
    }

    public void ExitState()
    {

    }
}
