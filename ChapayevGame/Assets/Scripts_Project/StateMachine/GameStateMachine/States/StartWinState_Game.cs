using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWinState_Game : IState
{
    private UIMiniGameSceneRoot sceneRoot;
    private IGlobalStateMachine stateMachine;
    private ISoundProvider soundProvider;

    private IEnumerator coroutineTimer;

    private ISound soundBackground_1;
    private ISound soundBackground_2;
    private ISound soundWin;
    
    public StartWinState_Game(IGlobalStateMachine stateMachine, UIMiniGameSceneRoot sceneRoot, ISoundProvider soundProvider)
    {
        this.sceneRoot = sceneRoot;
        this.stateMachine = stateMachine;
        this.soundProvider = soundProvider;

        soundBackground_1 = soundProvider.GetSound("Background_1");
        soundBackground_2 = soundProvider.GetSound("Background_2");
        soundWin = soundProvider.GetSound("WinGame");
    }

    public void EnterState()
    {
        Debug.Log("ACTIVATE STATE - START WIN");

        soundBackground_1.SetVolume(0.4f, 0, 0.5f, soundBackground_1.Stop);

        soundWin.Play();
        soundWin.SetVolume(0, 0.5f, 0.5f);

        sceneRoot.CloseGameArrowPanel();
        sceneRoot.CloseChipDownCountPanel();
        sceneRoot.CloseChipUpCountPanel();

        if (coroutineTimer != null)
            Coroutines.Stop(coroutineTimer);

        coroutineTimer = Timer(0.5f);
        Coroutines.Start(coroutineTimer);
    }

    public void ExitState()
    {
        Debug.Log("DEACTIVATE STATE - START WIN");

        if (coroutineTimer != null)
            Coroutines.Stop(coroutineTimer);
    }

    private IEnumerator Timer(float seconds)
    {
        yield return new WaitForSeconds(0.5f);

        soundBackground_2.Play();
        soundBackground_2.SetVolume(0, 0.4f, 0.5f);

        yield return new WaitForSeconds(seconds);

        ChangeStateToWin();
    }

    private void ChangeStateToWin()
    {
        stateMachine.SetState(stateMachine.GetState<WinState_Game>());
    }
}
