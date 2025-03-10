using System;
using UnityEngine;

public class MiniGameSceneEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private StrategyGroup strategyGroup;
    [SerializeField] private ChipGroup chipGroup;
    [SerializeField] private WinPrices winPrices;
    [SerializeField] private UIMiniGameSceneRoot sceneRootPrefab;

    private UIMiniGameSceneRoot sceneRoot;
    private ViewContainer viewContainer;
    private BankPresenter bankPresenter;


    private StoreStrategyPresenter storeStrategyPresenter;
    private StrategyBuyVisualizePresenter strategyBuyVisualizePresenter;
    private StrategyBuyPresenter strategyBuyPresenter;
    private StrategySelectPresenter strategySelectPresenter;

    private StoreChipPresenter storeChipPresenter;
    private ChipBuyVisualizePresenter chipBuyVisualizePresenter;
    private ChipBuyPresenter chipBuyPresenter;
    private ChipSelectPresenter chipSelectPresenter;



    private ChipSpawnerPresenter chipSpawnerPresenter_Player;
    private ChipSpawnerPresenter chipSpawnerPresenter_Bot;
    private ChipMovePresenter chipMovePresenter;

    private SpinMotionPresenter spinMotionPresenter;
    private FencePresenter fencePresenter;
    private GameResultPresenter gameResultPresenter;

    private GameStateMachine stateMachine;

    public void Run(UIRootView uIRootView)
    {
        sceneRoot = sceneRootPrefab;

        uIRootView.AttachSceneUI(sceneRoot.gameObject, Camera.main);

        sceneRoot.Activate();

        viewContainer = sceneRoot.GetComponent<ViewContainer>();
        viewContainer.Initialize();

        bankPresenter = new BankPresenter(new BankModel(), viewContainer.GetView<BankView>());

        storeStrategyPresenter = new StoreStrategyPresenter(new StoreStrategyModel(strategyGroup));
        strategyBuyPresenter = new StrategyBuyPresenter(new StrategyBuyModel(bankPresenter, storeStrategyPresenter), viewContainer.GetView<StrategyBuyView>());
        strategyBuyVisualizePresenter = new StrategyBuyVisualizePresenter(new StrategyBuyVisualizeModel(), viewContainer.GetView<StrategyBuyVisualizeView>());
        strategySelectPresenter = new StrategySelectPresenter(new StrategySelectModel(), viewContainer.GetView<StrategySelectView>());

        storeChipPresenter = new StoreChipPresenter(new StoreChipModel(chipGroup));
        chipBuyPresenter = new ChipBuyPresenter(new ChipBuyModel(bankPresenter, storeChipPresenter), viewContainer.GetView<ChipBuyView>());
        chipBuyVisualizePresenter = new ChipBuyVisualizePresenter(new ChipBuyVisualizeModel(), viewContainer.GetView<ChipBuyVisualizeView>());
        chipSelectPresenter = new ChipSelectPresenter(new ChipSelectModel(), viewContainer.GetView<ChipSelectView>());

        chipSpawnerPresenter_Player = new ChipSpawnerPresenter(new ChipSpawnerModel(), viewContainer.GetView<ChipSpawnerView>("Player"));
        chipSpawnerPresenter_Bot = new ChipSpawnerPresenter(new ChipSpawnerModel(), viewContainer.GetView<ChipSpawnerView>("Bot"));
        chipMovePresenter = new ChipMovePresenter(new ChipMoveModel(), viewContainer.GetView<ChipMoveView>());

        spinMotionPresenter = new SpinMotionPresenter(new SpinMotionModel(), viewContainer.GetView<SpinMotionView>()); 

        fencePresenter = new FencePresenter(new FenceModel(), viewContainer.GetView<FenceView>());

        gameResultPresenter = new GameResultPresenter(new GameResultModel(winPrices), viewContainer.GetView<GameResultView>());

        stateMachine = new GameStateMachine(
            sceneRoot, 
            spinMotionPresenter, 
            chipMovePresenter, 
            gameResultPresenter,
            storeChipPresenter,
            chipBuyVisualizePresenter,
            chipBuyPresenter,
            chipSelectPresenter,
            storeStrategyPresenter,
            strategyBuyVisualizePresenter,
            strategyBuyPresenter,
            strategySelectPresenter,
            chipSpawnerPresenter_Player,
            chipSpawnerPresenter_Bot);

        sceneRoot.Activate();
        sceneRoot.Initialize();

        ActivateEvents();

        chipSpawnerPresenter_Bot.Activate();
        chipSpawnerPresenter_Player.Activate();

        spinMotionPresenter.Initialize();
        fencePresenter.Initialize();
        gameResultPresenter.Initialize();

        storeStrategyPresenter.Initialize();

        chipMovePresenter.Initialize();
        chipSpawnerPresenter_Player.Initialize();
        chipSpawnerPresenter_Bot.Initialize();

        strategySelectPresenter.Initialize();
        strategyBuyVisualizePresenter.Initialize();
        strategyBuyPresenter.Initialize();
        storeStrategyPresenter.Initialize();

        chipSelectPresenter.Initialize();
        chipBuyVisualizePresenter.Initialize();
        chipBuyPresenter.Initialize();
        storeChipPresenter.Initialize();

        bankPresenter.Initialize();

        stateMachine.Initialize();

        fencePresenter.RandomFence();

    }

    private void ActivateEvents()
    {
        ActivateTransitionsSceneEvents();

        storeStrategyPresenter.OnSelectStrategy += chipSpawnerPresenter_Player.SetStrategy;
        storeChipPresenter.OnSelectChip += chipSpawnerPresenter_Player.SetChip;

        storeStrategyPresenter.OnSelectStrategy += chipSpawnerPresenter_Bot.SetStrategy;
        storeChipPresenter.OnSelectChip += chipSpawnerPresenter_Bot.SetChip;

        chipSpawnerPresenter_Player.OnSpawnChip += chipMovePresenter.AddChip;
        chipSpawnerPresenter_Player.OnDestroyChip += chipMovePresenter.RemoveChip;

        chipSpawnerPresenter_Bot.OnSpawnChip += gameResultPresenter.AddBotChip;
        chipSpawnerPresenter_Player.OnSpawnChip += gameResultPresenter.AddPlayerChip;
        chipSpawnerPresenter_Bot.OnDestroyChip += gameResultPresenter.RemoveBotChip;
        chipSpawnerPresenter_Player.OnDestroyChip += gameResultPresenter.RemovePlayerChip;

        storeStrategyPresenter.OnOpenStrategy += strategyBuyVisualizePresenter.SetOpenStrategy;
        storeStrategyPresenter.OnCloseStrategy += strategyBuyVisualizePresenter.SetCloseStrategy;
        storeStrategyPresenter.OnOpenStrategy += strategySelectPresenter.SetOpenStrategy;
        storeStrategyPresenter.OnSelectStrategy += strategySelectPresenter.SelectStrategy;
        storeStrategyPresenter.OnDeselectStrategy += strategySelectPresenter.DeselectStrategy;

        storeStrategyPresenter.OnSelectStrategy += chipSelectPresenter.SetStrategy;
        storeChipPresenter.OnOpenChip += chipBuyVisualizePresenter.SetOpenChip;
        storeChipPresenter.OnCloseChip += chipBuyVisualizePresenter.SetCloseChip;
        storeChipPresenter.OnOpenChip += chipSelectPresenter.SetOpenChip;
        storeChipPresenter.OnSelectChip += chipSelectPresenter.SelectChip;
        storeChipPresenter.OnDeselectChip += chipSelectPresenter.DeselectChip;
    }

    private void DeactivateEvents()
    {
        DeactivateTransitionsSceneEvents();

        storeStrategyPresenter.OnSelectStrategy -= chipSpawnerPresenter_Player.SetStrategy;
        storeChipPresenter.OnSelectChip -= chipSpawnerPresenter_Player.SetChip;

        storeStrategyPresenter.OnSelectStrategy -= chipSpawnerPresenter_Bot.SetStrategy;
        storeChipPresenter.OnSelectChip -= chipSpawnerPresenter_Bot.SetChip;

        chipSpawnerPresenter_Player.OnSpawnChip -= chipMovePresenter.AddChip;
        chipSpawnerPresenter_Player.OnDestroyChip -= chipMovePresenter.RemoveChip;

        chipSpawnerPresenter_Bot.OnSpawnChip -= gameResultPresenter.AddBotChip;
        chipSpawnerPresenter_Player.OnSpawnChip -= gameResultPresenter.AddPlayerChip;
        chipSpawnerPresenter_Bot.OnDestroyChip -= gameResultPresenter.RemoveBotChip;
        chipSpawnerPresenter_Player.OnDestroyChip -= gameResultPresenter.RemovePlayerChip;

        storeStrategyPresenter.OnOpenStrategy -= strategyBuyVisualizePresenter.SetOpenStrategy;
        storeStrategyPresenter.OnCloseStrategy -= strategyBuyVisualizePresenter.SetCloseStrategy;
        storeStrategyPresenter.OnOpenStrategy -= strategySelectPresenter.SetOpenStrategy;
        storeStrategyPresenter.OnSelectStrategy -= strategySelectPresenter.SelectStrategy;
        storeStrategyPresenter.OnDeselectStrategy -= strategySelectPresenter.DeselectStrategy;

        storeStrategyPresenter.OnSelectStrategy -= chipSelectPresenter.SetStrategy;
        storeChipPresenter.OnOpenChip -= chipBuyVisualizePresenter.SetOpenChip;
        storeChipPresenter.OnCloseChip -= chipBuyVisualizePresenter.SetCloseChip;
        storeChipPresenter.OnOpenChip -= chipSelectPresenter.SetOpenChip;
        storeChipPresenter.OnSelectChip -= chipSelectPresenter.SelectChip;
        storeChipPresenter.OnDeselectChip -= chipSelectPresenter.DeselectChip;
    }

    private void ActivateTransitionsSceneEvents()
    {
        sceneRoot.OnClickToPlay += HandleGoToGame;
    }

    private void DeactivateTransitionsSceneEvents()
    {
        sceneRoot.OnClickToPlay -= HandleGoToGame;
    }

    public void Dispose()
    {
        DeactivateEvents();

        sceneRoot?.Dispose();

        storeStrategyPresenter.Dispose();

        spinMotionPresenter?.Dispose();
        fencePresenter?.Dispose();

        chipMovePresenter?.Dispose();
        chipSpawnerPresenter_Player?.Dispose();
        chipSpawnerPresenter_Bot?.Dispose();
        storeChipPresenter.Dispose();

        stateMachine?.Dispose();
    }

    private void OnDestroy()
    {
        Dispose();
    }

    #region Input

    public event Action OnGoToGame;

    private void HandleGoToGame()
    {
        sceneRoot.Deactivate();
        OnGoToGame?.Invoke();
    }

    #endregion
}
