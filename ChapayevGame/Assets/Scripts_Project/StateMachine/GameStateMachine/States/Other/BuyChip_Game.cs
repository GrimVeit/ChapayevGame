using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyChip_Game : IState
{
    private readonly UIMiniGameSceneRoot sceneRoot;
    private readonly ChipBuyPresenter chipBuyPresenter;
    private readonly ChipBuyVisualizePresenter chipBuyVisualizePresenter;
    private readonly StoreChipPresenter storeChipPresenter;

    private readonly IGlobalStateMachine stateMachine;

    public BuyChip_Game(IGlobalStateMachine stateMachine, UIMiniGameSceneRoot sceneRoot, ChipBuyPresenter chipBuyPresenter, StoreChipPresenter storeChipPresenter, ChipBuyVisualizePresenter chipBuyVisualizePresenter)
    {
        this.sceneRoot = sceneRoot;
        this.chipBuyPresenter = chipBuyPresenter;
        this.storeChipPresenter = storeChipPresenter;
        this.stateMachine = stateMachine;
        this.chipBuyVisualizePresenter = chipBuyVisualizePresenter;
    }

    public void EnterState()
    {
        sceneRoot.OnClickToBackFromBuyChip += ChangeStateToWin;
        chipBuyPresenter.OnBuyChip += storeChipPresenter.OpenChip;

        sceneRoot.OpenStoreChipPanel();
    }

    public void ExitState()
    {
        sceneRoot.OnClickToBackFromBuyChip -= ChangeStateToWin;
        chipBuyPresenter.OnBuyChip -= storeChipPresenter.OpenChip;
    }

    private void ChangeStateToWin()
    {
        stateMachine.SetState(stateMachine.GetState<WinState_Game>());
    }

    private void ChangeStateToLose()
    {
        stateMachine.SetState(stateMachine.GetState<LoseState_Game>());
    }
}
