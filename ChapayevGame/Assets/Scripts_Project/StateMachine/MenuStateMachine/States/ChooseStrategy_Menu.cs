using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseStrategy_Menu : IState
{
    private UIMainMenuRoot sceneRoot;
    private StoreStrategyPresenter storeStrategyPresenter;
    private StoreChipPresenter storeChipPresenter;
    private StrategySelectPresenter strategySelectPresenter;

    private ITutorialDescriptionProvider tutorialDescriptionProvider;
    private IGlobalStateMachine stateMachine;

    public ChooseStrategy_Menu(IGlobalStateMachine stateMachine, UIMainMenuRoot sceneRoot, StoreStrategyPresenter storeStrategyPresenter, StrategySelectPresenter strategySelectPresenter, StoreChipPresenter storeChipPresenter, ITutorialDescriptionProvider tutorialDescriptionProvider)
    {
        this.sceneRoot = sceneRoot;
        this.storeStrategyPresenter = storeStrategyPresenter;
        this.strategySelectPresenter = strategySelectPresenter;
        this.stateMachine = stateMachine;
        this.storeChipPresenter = storeChipPresenter;
        this.tutorialDescriptionProvider = tutorialDescriptionProvider;
    }

    public void EnterState()
    {
        sceneRoot.OnClickToCancelFromChooseStrategy += ChangeStateToMain;
        sceneRoot.OnClickToOpenChooseChipFromChooseStrategy += ChangeStateToChooseChip;

        strategySelectPresenter.OnChooseStrategy += storeStrategyPresenter.SelectStrategy;

        tutorialDescriptionProvider.ActivateTutorial("ChooseStrategy");
        storeChipPresenter.UnselectAllChips();
        sceneRoot.OpenChooseStrategyPanel();
    }

    public void ExitState()
    {
        sceneRoot.OnClickToCancelFromChooseStrategy -= ChangeStateToMain;
        sceneRoot.OnClickToOpenChooseChipFromChooseStrategy -= ChangeStateToChooseChip;

        strategySelectPresenter.OnChooseStrategy -= storeStrategyPresenter.SelectStrategy;

        tutorialDescriptionProvider.DeactivateTutorial("ChooseStrategy");
    }

    private void ChangeStateToChooseChip()
    {
        stateMachine.SetState(stateMachine.GetState<ChooseChip_Menu>());
    }

    private void ChangeStateToMain()
    {
        stateMachine.SetState(stateMachine.GetState<MainState_Menu>());
    }
}
