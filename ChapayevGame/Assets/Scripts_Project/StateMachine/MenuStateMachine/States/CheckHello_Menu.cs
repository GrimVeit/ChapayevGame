using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHello_Menu : IState
{
    private IGlobalStateMachine stateMachine;
    private ITutorialDescriptionProvider tutorialDescriptionProvider;

    public CheckHello_Menu(IGlobalStateMachine stateMachine, ITutorialDescriptionProvider tutorialDescriptionProvider)
    {
        this.stateMachine = stateMachine;
        this.tutorialDescriptionProvider = tutorialDescriptionProvider;
    }

    public void EnterState()
    {
        if (tutorialDescriptionProvider.IsActiveTutorial("Hello"))
        {
            tutorialDescriptionProvider.LockTutorial("Hello");

            ChangeStateToHello();
        }
        else
        {
            ChangeStateToMain();
        }
    }

    public void ExitState()
    {

    }

    private void ChangeStateToMain()
    {
        stateMachine.SetState(stateMachine.GetState<MainState_Menu>());
    }

    private void ChangeStateToHello()
    {
        stateMachine.SetState(stateMachine.GetState<Hello_Menu>());
    }
}
