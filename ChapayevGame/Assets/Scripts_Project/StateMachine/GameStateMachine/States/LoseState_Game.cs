using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseState_Game : IState
{
    private UIGameRoot sceneRoot;

    private ChipSpawnerPresenter chipSpawnerPresenter_Bot;
    private ChipSpawnerPresenter chipSpawnerPresenter_Player;

    private StoreChipPresenter storeChipPresenter;
    private StoreStrategyPresenter storeStrategyPresenter;
    private BotStoreChipPresenter botStoreChipPresenter;

    private IGlobalStateMachine stateMachine;

    public LoseState_Game(IGlobalStateMachine stateMachine, UIGameRoot sceneRoot, StoreStrategyPresenter storeStrategyPresenter, StoreChipPresenter storeChipPresenter, ChipSpawnerPresenter chipSpawnerPresenter_Player, ChipSpawnerPresenter chipSpawnerPresenter_Bot, BotStoreChipPresenter botStoreChipPresenter)
    {
        this.stateMachine = stateMachine;
        this.sceneRoot = sceneRoot;

        this.storeChipPresenter = storeChipPresenter;
        this.storeStrategyPresenter = storeStrategyPresenter;
        this.chipSpawnerPresenter_Player = chipSpawnerPresenter_Player;
        this.chipSpawnerPresenter_Bot = chipSpawnerPresenter_Bot;
        this.botStoreChipPresenter = botStoreChipPresenter;
    }

    public void EnterState()
    {
        Debug.Log("ACTIVATE STATE - LOSE");

        sceneRoot.OnClickToOpenBuyStrategy_LosePanel += ChangeStateToBuyStrategy;
        sceneRoot.OnClickToOpenBuyChip_LosePanel += ChangeStateToBuyChip;
        sceneRoot.OnClickToOpenChooseStrategy_LosePanel += ChangeStateToChooseStrategy;

        chipSpawnerPresenter_Bot.Deactivate();
        chipSpawnerPresenter_Player.Deactivate();
        botStoreChipPresenter.Deactivate();

        storeStrategyPresenter.UnselectAllStrategies();
        storeChipPresenter.UnselectAllChips();
        sceneRoot.CloseGameArrowPanel();
        sceneRoot.OpenLosePanel();
    }

    public void ExitState()
    {
        Debug.Log("DEACTIVATE STATE - LOSE");

        sceneRoot.OnClickToOpenBuyStrategy_LosePanel -= ChangeStateToBuyStrategy;
        sceneRoot.OnClickToOpenBuyChip_LosePanel -= ChangeStateToBuyChip;
        sceneRoot.OnClickToOpenChooseStrategy_LosePanel -= ChangeStateToChooseStrategy;
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
