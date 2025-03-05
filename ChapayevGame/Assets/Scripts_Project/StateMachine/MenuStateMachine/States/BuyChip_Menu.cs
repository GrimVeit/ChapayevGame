using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyChip_Menu : IState
{
    private readonly UIMainMenuRoot sceneRoot;
    private readonly ChipBuyPresenter chipBuyPresenter;
    private readonly ChipBuyVisualizePresenter chipBuyVisualizePresenter;
    private readonly StoreChipPresenter storeChipPresenter;

    private readonly IGlobalStateMachine stateMachine;

    public BuyChip_Menu(IGlobalStateMachine stateMachine, UIMainMenuRoot sceneRoot, ChipBuyPresenter chipBuyPresenter, StoreChipPresenter storeChipPresenter, ChipBuyVisualizePresenter chipBuyVisualizePresenter)
    {
        this.sceneRoot = sceneRoot;
        this.chipBuyPresenter = chipBuyPresenter;
        this.storeChipPresenter = storeChipPresenter;
        this.stateMachine = stateMachine;
        this.chipBuyVisualizePresenter = chipBuyVisualizePresenter;
    }

    public void EnterState()
    {
        sceneRoot.OnClickToBackFromBuyChip += ChangeStateToMain;
        chipBuyPresenter.OnBuyChip += storeChipPresenter.OpenChip;

        sceneRoot.OpenStoreChipPanel();
    }

    public void ExitState()
    {
        sceneRoot.OnClickToBackFromBuyChip -= ChangeStateToMain;
        chipBuyPresenter.OnBuyChip -= storeChipPresenter.OpenChip;
    }

    private void ChangeStateToMain()
    {
        stateMachine.SetState(stateMachine.GetState<MainState_Menu>());
    }
}
