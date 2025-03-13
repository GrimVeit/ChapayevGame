using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseChip_Menu : IState
{
    private UIMainMenuRoot sceneRoot;
    private StoreChipPresenter storeChipPresentergyPresenter;
    private ChipSelectPresenter chipSelectPresenter;

    private ITutorialDescriptionProvider tutorialDescriptionProvider;
    private IGlobalStateMachine stateMachine;

    public ChooseChip_Menu(IGlobalStateMachine stateMachine, UIMainMenuRoot sceneRoot, StoreChipPresenter storeChipPresenter, ChipSelectPresenter chipSelectPresenter, ITutorialDescriptionProvider tutorialDescriptionProvider)
    {
        this.sceneRoot = sceneRoot;
        this.storeChipPresentergyPresenter = storeChipPresenter;
        this.chipSelectPresenter = chipSelectPresenter;
        this.stateMachine = stateMachine;
        this.tutorialDescriptionProvider = tutorialDescriptionProvider;
    }

    public void EnterState()
    {
        sceneRoot.OnClickToOpenChooseStrategyFromChooseChip += ChangeStateToChooseStrategy;

        chipSelectPresenter.OnChooseChip += storeChipPresentergyPresenter.SelectChip;

        sceneRoot.OpenChooseChipPanel();

        tutorialDescriptionProvider.ActivateTutorial("ChooseChips");
    }

    public void ExitState()
    {
        sceneRoot.OnClickToOpenChooseStrategyFromChooseChip -= ChangeStateToChooseStrategy;

        chipSelectPresenter.OnChooseChip -= storeChipPresentergyPresenter.SelectChip;

        tutorialDescriptionProvider.DeactivateTutorial("ChooseChips");
    }

    private void ChangeStateToChooseChip()
    {

    }

    private void ChangeStateToChooseStrategy()
    {
        stateMachine.SetState(stateMachine.GetState<ChooseStrategy_Menu>());
    }
}
