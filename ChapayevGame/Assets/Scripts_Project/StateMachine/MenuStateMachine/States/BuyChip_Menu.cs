public class BuyChip_Menu : IState
{
    private readonly UIMenuRoot sceneRoot;
    private readonly ChipBuyPresenter chipBuyPresenter;
    private readonly ChipBuyVisualizePresenter chipBuyVisualizePresenter;
    private readonly StoreChipPresenter storeChipPresenter;

    private readonly IGlobalStateMachine stateMachine;

    public BuyChip_Menu(IGlobalStateMachine stateMachine, UIMenuRoot sceneRoot, ChipBuyPresenter chipBuyPresenter, StoreChipPresenter storeChipPresenter, ChipBuyVisualizePresenter chipBuyVisualizePresenter)
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
        chipBuyPresenter.OnSelectRandom += ChangeStateToLoadBuyChip;

        sceneRoot.OpenStoreChipPanel();
    }

    public void ExitState()
    {
        sceneRoot.OnClickToBackFromBuyChip -= ChangeStateToMain;
        chipBuyPresenter.OnSelectRandom -= ChangeStateToLoadBuyChip;
    }

    private void ChangeStateToMain()
    {
        stateMachine.SetState(stateMachine.GetState<MainState_Menu>());
    }

    private void ChangeStateToLoadBuyChip()
    {
        stateMachine.SetState(stateMachine.GetState<LoadBuyChip_Menu>());
    }
}
