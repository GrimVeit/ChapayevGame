using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState_Game : IState
{
    private UIMiniGameSceneRoot sceneRoot;

    private ChipSpawnerPresenter chipSpawnerPresenter_Bot;
    private ChipSpawnerPresenter chipSpawnerPresenter_Player;

    private StoreChipPresenter storeChipPresenter;
    private StoreStrategyPresenter storeStrategyPresenter;

    private IGlobalStateMachine stateMachine;

    public WinState_Game(IGlobalStateMachine stateMachine, UIMiniGameSceneRoot sceneRoot, StoreChipPresenter storeChipPresenter, StoreStrategyPresenter storeStrategyPresenter, ChipSpawnerPresenter chipSpawnerPresenter_Player, ChipSpawnerPresenter chipSpawnerPresenter_Bot)
    {
        this.stateMachine = stateMachine;
        this.sceneRoot = sceneRoot;
        this.storeChipPresenter = storeChipPresenter;
        this.storeStrategyPresenter = storeStrategyPresenter;
        this.chipSpawnerPresenter_Player = chipSpawnerPresenter_Player;
        this.chipSpawnerPresenter_Bot = chipSpawnerPresenter_Bot;
    }

    public void EnterState()
    {
        Debug.Log("ACTIVATE STATE - WIN");

        sceneRoot.OnClickToOpenBuyStrategy_WinPanel += ChangeStateToBuyStrategy;
        sceneRoot.OnClickToOpenBuyChip_WinPanel += ChangeStateToBuyChip;
        sceneRoot.OnClickToOpenChooseStrategy_WinPanel += ChangeStateToChooseStrategy;

        chipSpawnerPresenter_Bot.Deactivate();
        chipSpawnerPresenter_Player.Deactivate();

        storeChipPresenter.UnselectAllChips();
        storeStrategyPresenter.UnselectAllStrategies();
        sceneRoot.OpenWinPanel();
    }

    public void ExitState()
    {
        Debug.Log("DEACTIVATE STATE - WIN");

        sceneRoot.OnClickToOpenBuyStrategy_WinPanel -= ChangeStateToBuyStrategy;
        sceneRoot.OnClickToOpenBuyChip_WinPanel -= ChangeStateToBuyChip;
        sceneRoot.OnClickToOpenChooseStrategy_WinPanel -= ChangeStateToChooseStrategy;
    }

    private void ChangeStateToBuyStrategy()
    {
        stateMachine.SetState(stateMachine.GetState<BuyStrategy_Game>());
    }

    private void ChangeStateToBuyChip()
    {
        stateMachine.SetState(stateMachine.GetState<BuyChip_Game>());
    }

    private void ChangeStateToChooseStrategy()
    {
        stateMachine.SetState(stateMachine.GetState<ChooseStrategy_Game>());
    }
}
