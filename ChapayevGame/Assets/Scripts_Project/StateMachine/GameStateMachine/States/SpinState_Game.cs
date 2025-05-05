using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinState_Game : IState
{
    private UIGameRoot sceneRoot;

    private readonly IGlobalStateMachine stateMachine;
    private SpinMotionPresenter spinMotionPresenter;
    private ISoundProvider soundProvider;

    private ITutorialDescriptionProvider tutorialDescriptionPresenter;

    public SpinState_Game(IGlobalStateMachine stateMachine, UIGameRoot sceneRoot, SpinMotionPresenter motionPresenter, ITutorialDescriptionProvider descriptionProvider, ISoundProvider soundProvider)
    {
        this.stateMachine = stateMachine;
        this.sceneRoot = sceneRoot;
        this.spinMotionPresenter = motionPresenter;
        this.tutorialDescriptionPresenter = descriptionProvider;
        this.soundProvider = soundProvider;
    }

    public void EnterState()
    {
        Debug.Log("ACTIVATE STATE - SPIN");

        soundProvider.PlayOneShot("Wheel");

        spinMotionPresenter.OnBotMotion += ChangeStateToBotMotionState;
        spinMotionPresenter.OnPlayerMotion += ChangeStateToPlayerMotionState;

        spinMotionPresenter.ActivateSpin();
        tutorialDescriptionPresenter.ActivateTutorial("SpinWheel");
    }

    public void ExitState()
    {
        Debug.Log("DEACTIVATE STATE - SPIN");

        spinMotionPresenter.OnBotMotion -= ChangeStateToBotMotionState;
        spinMotionPresenter.OnPlayerMotion -= ChangeStateToPlayerMotionState;

        sceneRoot.CloseSpinPanel();
        sceneRoot.OpenGameArrowPanel();
        
        sceneRoot.OpenChipDownCountPanel();
        sceneRoot.OpenChipUpCountPanel();

        tutorialDescriptionPresenter.LockTutorial("SpinWheel");
    }

    private void ChangeStateToBotMotionState()
    {
        stateMachine.SetState(stateMachine.GetState<BotMotionState_Game>());
    }

    private void ChangeStateToPlayerMotionState()
    {
        stateMachine.SetState(stateMachine.GetState<PlayerMotionState_Game>());
    }
}
