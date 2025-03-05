using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseChip_Menu : IState
{
    private UIMainMenuRoot sceneRoot;
    private StoreChipPresenter storeChipPresentergyPresenter;
    private ChipSelectPresenter chipSelectPresenter;

    private IGlobalStateMachine stateMachine;

    public ChooseChip_Menu(IGlobalStateMachine stateMachine, UIMainMenuRoot sceneRoot, StoreChipPresenter storeChipPresenter, ChipSelectPresenter chipSelectPresenter)
    {
        this.sceneRoot = sceneRoot;
        this.storeChipPresentergyPresenter = storeChipPresenter;
        this.chipSelectPresenter = chipSelectPresenter;
        this.stateMachine = stateMachine;
    }

    public void EnterState()
    {
        sceneRoot.OnClickToOpenChooseStrategyFromChooseChip += ChangeStateToChooseStrategy;

        chipSelectPresenter.OnChooseChip += storeChipPresentergyPresenter.SelectChip;

        sceneRoot.OpenChooseChipPanel();
    }

    public void ExitState()
    {
        sceneRoot.OnClickToOpenChooseStrategyFromChooseChip -= ChangeStateToChooseStrategy;

        chipSelectPresenter.OnChooseChip -= storeChipPresentergyPresenter.SelectChip;
    }

    private void ChangeStateToChooseChip()
    {

    }

    private void ChangeStateToChooseStrategy()
    {
        stateMachine.SetState(stateMachine.GetState<ChooseStrategy_Menu>());
    }
}
