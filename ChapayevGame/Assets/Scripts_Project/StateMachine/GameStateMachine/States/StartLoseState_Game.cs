using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLoseState_Game : IState
{
    private UIGameRoot sceneRoot;
    private IGlobalStateMachine stateMachine;
    private ISoundProvider soundProvider;

    private IEnumerator coroutineTimer;

    private ISound soundBackground;
    private ISound soundLose;

    public StartLoseState_Game(IGlobalStateMachine stateMachine, UIGameRoot sceneRoot, ISoundProvider soundProvider)
    {
        this.sceneRoot = sceneRoot;
        this.stateMachine = stateMachine;
        this.soundProvider = soundProvider;

        soundBackground = soundProvider.GetSound("Background_1");
        soundLose = soundProvider.GetSound("LoseGame");
    }

    public void EnterState()
    {
        soundBackground.SetVolume(0.4f, 0, 0.2f, soundBackground.Stop);

        soundLose.Play();
        soundLose.SetVolume(0, 0.5f, 0.1f);

        sceneRoot.CloseGameArrowPanel();
        sceneRoot.CloseChipDownCountPanel();
        sceneRoot.CloseChipUpCountPanel();

        if (coroutineTimer != null)
            Coroutines.Stop(coroutineTimer);

        coroutineTimer = Timer(1);
        Coroutines.Start(coroutineTimer);
    }

    public void ExitState()
    {
        if (coroutineTimer != null)
            Coroutines.Stop(coroutineTimer);
    }

    private IEnumerator Timer(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        ChangeStateToLose();
    }

    private void ChangeStateToLose()
    {
        stateMachine.SetState(stateMachine.GetState<LoseState_Game>());
    }
}
