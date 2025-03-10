using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseChip_Game : IState
{
    private UIMiniGameSceneRoot sceneRoot;
    private StoreChipPresenter storeChipPresentergyPresenter;
    private ChipSelectPresenter chipSelectPresenter;

    private IGlobalStateMachine stateMachine;

    public ChooseChip_Game(IGlobalStateMachine stateMachine, UIMiniGameSceneRoot sceneRoot, StoreChipPresenter storeChipPresenter, ChipSelectPresenter chipSelectPresenter)
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

    private void ChangeStateToChooseStrategy()
    {
        stateMachine.SetState(stateMachine.GetState<ChooseStrategy_Game>());
    }
}
