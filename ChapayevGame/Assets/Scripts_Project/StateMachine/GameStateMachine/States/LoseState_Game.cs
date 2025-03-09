using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseState_Game : IState
{
    private readonly UIMiniGameSceneRoot sceneRoot;

    public LoseState_Game(UIMiniGameSceneRoot sceneRoot)
    {
        this.sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        sceneRoot.OpenLosePanel();
    }

    public void ExitState()
    {

    }
}
