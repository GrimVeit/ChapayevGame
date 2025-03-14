public class BuyChip_Game : IState
{
    private readonly UIMiniGameSceneRoot sceneRoot;
    private readonly ChipBuyPresenter chipBuyPresenter;
    private readonly ChipBuyVisualizePresenter chipBuyVisualizePresenter;
    private readonly StoreChipPresenter storeChipPresenter;
    private readonly GameResultPresenter gameResultPresenter;

    private readonly IGlobalStateMachine stateMachine;

    public BuyChip_Game(IGlobalStateMachine stateMachine, UIMiniGameSceneRoot sceneRoot, ChipBuyPresenter chipBuyPresenter, StoreChipPresenter storeChipPresenter, ChipBuyVisualizePresenter chipBuyVisualizePresenter, GameResultPresenter gameResultPresenter)
    {
        this.sceneRoot = sceneRoot;
        this.chipBuyPresenter = chipBuyPresenter;
        this.storeChipPresenter = storeChipPresenter;
        this.stateMachine = stateMachine;
        this.chipBuyVisualizePresenter = chipBuyVisualizePresenter;
        this.gameResultPresenter = gameResultPresenter;
    }

    public void EnterState()
    {
        sceneRoot.OnClickToBackFromBuyChip += CheckGameResult;
        chipBuyPresenter.OnSelectRandom += ChangeStateToLoadBuyChip;

        sceneRoot.OpenStoreChipPanel();
    }

    public void ExitState()
    {
        sceneRoot.OnClickToBackFromBuyChip -= CheckGameResult;
        chipBuyPresenter.OnSelectRandom -= ChangeStateToLoadBuyChip;
    }

    private void CheckGameResult()
    {
        if (gameResultPresenter.IsPlayerWin())
        {
            ChangeStateToWin();
        }
        else
        {
            ChangeStateToLose();
        }
    }

    private void ChangeStateToWin()
    {
        stateMachine.SetState(stateMachine.GetState<WinState_Game>());
    }

    private void ChangeStateToLose()
    {
        stateMachine.SetState(stateMachine.GetState<LoseState_Game>());
    }

    private void ChangeStateToLoadBuyChip()
    {
        stateMachine.SetState(stateMachine.GetState<LoadBuyChip_Game>());
    }
}
