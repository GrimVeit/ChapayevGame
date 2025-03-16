using System;
using Unity.VisualScripting;
using UnityEngine;

public class MiniGameSceneEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private StrategyGroup strategyGroup;
    [SerializeField] private ChipGroup chipGroup;
    [SerializeField] private WinPrices winPrices;
    [SerializeField] private TutorialDescriptionGroup tutorialDescriptionGroup;
    [SerializeField] private UIMiniGameSceneRoot sceneRootPrefab;

    private UIMiniGameSceneRoot sceneRoot;
    private ViewContainer viewContainer;
    private BankPresenter bankPresenter;
    private ParticleEffectPresenter particleEffectPresenter;

    private BotStoreStrategyPresenter botStoreStrategyPresenter;
    private StoreStrategyPresenter storeStrategyPresenter;
    private StrategyBuyVisualizePresenter strategyBuyVisualizePresenter;
    private StrategyBuyPresenter strategyBuyPresenter;
    private StrategySelectPresenter strategySelectPresenter;

    private BotStoreChipPresenter botStoreChipPresenter;
    private StoreChipPresenter storeChipPresenter;
    private ChipBuyVisualizePresenter chipBuyVisualizePresenter;
    private ChipBuyPresenter chipBuyPresenter;
    private ChipSelectPresenter chipSelectPresenter;



    private ChipSpawnerPresenter chipSpawnerPresenter_Player;
    private ChipSpawnerPresenter chipSpawnerPresenter_Bot;

    private ChipMovePresenter chipMovePresenter;
    private ChipBotMovePresenter chipBotMovePresenter;

    private SpinMotionPresenter spinMotionPresenter;
    private FencePresenter fencePresenter;
    private GameResultPresenter gameResultPresenter;
    private ChipPunchPresenter chipPunchPresenter;

    private TutorialDescriptionPresenter tutorialDescriptionPresenter;
    private AnimationFramePresenter animationFramePresenter;
    private GameArrowPresenter gameArrowPresenter;

    private ChipCounterPresenter chipCounterPresenter_Player;
    private ChipCounterPresenter chipCounterPresenter_Bot;

    private GameStateMachine stateMachine;

    public void Run(UIRootView uIRootView)
    {
        sceneRoot = sceneRootPrefab;

        uIRootView.AttachSceneUI(sceneRoot.gameObject, Camera.main);

        sceneRoot.Activate();

        viewContainer = sceneRoot.GetComponent<ViewContainer>();
        viewContainer.Initialize();

        bankPresenter = new BankPresenter(new BankModel(), viewContainer.GetView<BankView>());
        particleEffectPresenter = new ParticleEffectPresenter(new ParticleEffectModel(), viewContainer.GetView<ParticleEffectView>());

        gameArrowPresenter = new GameArrowPresenter(new GameArrowModel(), viewContainer.GetView<GameArrowView>());
        animationFramePresenter = new AnimationFramePresenter(new AnimationFrameModel(), viewContainer.GetView<AnimationFrameView>());
        tutorialDescriptionPresenter = new TutorialDescriptionPresenter(new TutorialDescriptionModel(tutorialDescriptionGroup), viewContainer.GetView<TutorialDescriptionView>());

        botStoreStrategyPresenter = new BotStoreStrategyPresenter(new BotStoreStrategyModel(strategyGroup));
        storeStrategyPresenter = new StoreStrategyPresenter(new StoreStrategyModel(strategyGroup));
        strategyBuyPresenter = new StrategyBuyPresenter(new StrategyBuyModel(bankPresenter, storeStrategyPresenter), viewContainer.GetView<StrategyBuyView>());
        strategyBuyVisualizePresenter = new StrategyBuyVisualizePresenter(new StrategyBuyVisualizeModel(), viewContainer.GetView<StrategyBuyVisualizeView>());
        strategySelectPresenter = new StrategySelectPresenter(new StrategySelectModel(tutorialDescriptionPresenter), viewContainer.GetView<StrategySelectView>());

        botStoreChipPresenter = new BotStoreChipPresenter(new BotStoreChipModel(chipGroup));
        storeChipPresenter = new StoreChipPresenter(new StoreChipModel(chipGroup));
        chipBuyPresenter = new ChipBuyPresenter(new ChipBuyModel(bankPresenter, storeChipPresenter), viewContainer.GetView<ChipBuyView>());
        chipBuyVisualizePresenter = new ChipBuyVisualizePresenter(new ChipBuyVisualizeModel(), viewContainer.GetView<ChipBuyVisualizeView>());
        chipSelectPresenter = new ChipSelectPresenter(new ChipSelectModel(tutorialDescriptionPresenter), viewContainer.GetView<ChipSelectView>());

        chipSpawnerPresenter_Player = new ChipSpawnerPresenter(new ChipSpawnerModel(), viewContainer.GetView<ChipSpawnerView>("Player"));
        chipSpawnerPresenter_Bot = new ChipSpawnerPresenter(new ChipSpawnerModel(), viewContainer.GetView<ChipSpawnerView>("Bot"));

        chipMovePresenter = new ChipMovePresenter(new ChipMoveModel(tutorialDescriptionPresenter), viewContainer.GetView<ChipMoveView>());
        chipBotMovePresenter = new ChipBotMovePresenter(new ChipBotMoveModel(chipSpawnerPresenter_Bot, chipSpawnerPresenter_Player));

        chipPunchPresenter = new ChipPunchPresenter(new ChipPunchModel(), viewContainer.GetView<ChipPunchView>());

        spinMotionPresenter = new SpinMotionPresenter(new SpinMotionModel(), viewContainer.GetView<SpinMotionView>()); 

        fencePresenter = new FencePresenter(new FenceModel(), viewContainer.GetView<FenceView>());

        gameResultPresenter = new GameResultPresenter(new GameResultModel(winPrices, bankPresenter), viewContainer.GetView<GameResultView>());

        chipCounterPresenter_Player = new ChipCounterPresenter(new ChipCounterModel(), viewContainer.GetView<ChipCounterView>("Player"));
        chipCounterPresenter_Bot = new ChipCounterPresenter(new ChipCounterModel(), viewContainer.GetView<ChipCounterView>("Bot"));

        stateMachine = new GameStateMachine(
            sceneRoot, 
            spinMotionPresenter, 
            chipBotMovePresenter,
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
            chipSpawnerPresenter_Bot,
            tutorialDescriptionPresenter,
            animationFramePresenter,
            particleEffectPresenter,
            gameArrowPresenter);

        sceneRoot.Activate();
        sceneRoot.Initialize();

        ActivateEvents();

        chipCounterPresenter_Player.Initialize();
        chipCounterPresenter_Bot.Initialize();

        chipSpawnerPresenter_Bot.Activate();
        chipSpawnerPresenter_Player.Activate();

        gameArrowPresenter.Initialize();
        animationFramePresenter.Initialize();
        tutorialDescriptionPresenter.Initialize();

        chipPunchPresenter.Initialize();
        spinMotionPresenter.Initialize();
        fencePresenter.Initialize();
        gameResultPresenter.Initialize();

        botStoreStrategyPresenter.Initialize();

        chipBotMovePresenter.Initialize();
        chipMovePresenter.Initialize();
        chipSpawnerPresenter_Player.Initialize();
        chipSpawnerPresenter_Bot.Initialize();

        strategySelectPresenter.Initialize();
        strategyBuyVisualizePresenter.Initialize();
        strategyBuyPresenter.Initialize();
        storeStrategyPresenter.Initialize();
        botStoreStrategyPresenter.Initialize();

        chipSelectPresenter.Initialize();
        chipBuyVisualizePresenter.Initialize();
        chipBuyPresenter.Initialize();
        storeChipPresenter.Initialize();
        botStoreChipPresenter.Initialize();

        bankPresenter.Initialize();
        particleEffectPresenter.Initialize();

        stateMachine.Initialize();

        fencePresenter.RandomFence();

    }

    private void ActivateEvents()
    {
        ActivateTransitionsSceneEvents();

        storeStrategyPresenter.OnSelectStrategy += chipSpawnerPresenter_Player.SetStrategy;
        storeChipPresenter.OnSelectChip += chipSpawnerPresenter_Player.SetChip;

        storeStrategyPresenter.OnSelectStrategy += botStoreStrategyPresenter.SelectRandomStrategy;
        botStoreStrategyPresenter.OnSelectRandomStrategy += chipSpawnerPresenter_Bot.SetStrategy;

        storeChipPresenter.OnSelectChip += botStoreChipPresenter.SelectRandomChip;
        botStoreChipPresenter.OnSelectRandomChip += chipSpawnerPresenter_Bot.SetChip;





        chipSpawnerPresenter_Player.OnSpawnChip += chipMovePresenter.AddChip;
        chipSpawnerPresenter_Player.OnDestroyChip += chipMovePresenter.RemoveChip;

        chipSpawnerPresenter_Bot.OnSpawnChip += gameResultPresenter.AddBotChip;
        chipSpawnerPresenter_Player.OnSpawnChip += gameResultPresenter.AddPlayerChip;
        chipSpawnerPresenter_Bot.OnDestroyChip += gameResultPresenter.RemoveBotChip;
        chipSpawnerPresenter_Player.OnDestroyChip += gameResultPresenter.RemovePlayerChip;

        chipSpawnerPresenter_Bot.OnSpawnChip += chipCounterPresenter_Bot.AddChip;
        chipSpawnerPresenter_Player.OnSpawnChip += chipCounterPresenter_Player.AddChip;
        chipSpawnerPresenter_Bot.OnDestroyChip += chipCounterPresenter_Bot.RemoveChip;
        chipSpawnerPresenter_Player.OnDestroyChip += chipCounterPresenter_Player.RemoveChip;

        storeStrategyPresenter.OnOpenStrategy += strategyBuyVisualizePresenter.SetOpenStrategy;
        storeStrategyPresenter.OnOpenNewStrategy += strategyBuyVisualizePresenter.SetOpenNewStrategy;
        storeStrategyPresenter.OnCloseStrategy += strategyBuyVisualizePresenter.SetCloseStrategy;
        storeStrategyPresenter.OnOpenStrategy += strategySelectPresenter.SetOpenStrategy;
        storeStrategyPresenter.OnOpenNewStrategy += strategySelectPresenter.SetOpenNewStrategy;
        storeStrategyPresenter.OnSelectStrategy += strategySelectPresenter.SelectStrategy;
        storeStrategyPresenter.OnDeselectStrategy += strategySelectPresenter.DeselectStrategy;

        storeStrategyPresenter.OnSelectStrategy += chipSelectPresenter.SetStrategy;
        storeChipPresenter.OnOpenChip += chipBuyVisualizePresenter.SetOpenChip;
        storeChipPresenter.OnOpenNewChip += chipBuyVisualizePresenter.SetOpenNewChip;
        storeChipPresenter.OnCloseChip += chipBuyVisualizePresenter.SetCloseChip;
        storeChipPresenter.OnOpenChip += chipSelectPresenter.SetOpenChip;
        storeChipPresenter.OnOpenNewChip += chipSelectPresenter.SetOpenNewChip;
        storeChipPresenter.OnSelectChip += chipSelectPresenter.SelectChip;
        storeChipPresenter.OnDeselectChip += chipSelectPresenter.DeselectChip;

        chipSpawnerPresenter_Bot.OnPunch += chipPunchPresenter.AddPunchChip;
        chipSpawnerPresenter_Player.OnPunch += chipPunchPresenter.AddPunchChip;
    }

    private void DeactivateEvents()
    {
        DeactivateTransitionsSceneEvents();

        storeStrategyPresenter.OnSelectStrategy -= chipSpawnerPresenter_Player.SetStrategy;
        storeChipPresenter.OnSelectChip -= chipSpawnerPresenter_Player.SetChip;

        storeStrategyPresenter.OnSelectStrategy -= botStoreStrategyPresenter.SelectRandomStrategy;
        botStoreStrategyPresenter.OnSelectRandomStrategy -= chipSpawnerPresenter_Bot.SetStrategy;

        storeChipPresenter.OnSelectChip -= botStoreChipPresenter.SelectRandomChip;
        botStoreChipPresenter.OnSelectRandomChip -= chipSpawnerPresenter_Bot.SetChip;




        chipSpawnerPresenter_Player.OnSpawnChip -= chipMovePresenter.AddChip;
        chipSpawnerPresenter_Player.OnDestroyChip -= chipMovePresenter.RemoveChip;

        chipSpawnerPresenter_Bot.OnSpawnChip -= gameResultPresenter.AddBotChip;
        chipSpawnerPresenter_Player.OnSpawnChip -= gameResultPresenter.AddPlayerChip;
        chipSpawnerPresenter_Bot.OnDestroyChip -= gameResultPresenter.RemoveBotChip;
        chipSpawnerPresenter_Player.OnDestroyChip -= gameResultPresenter.RemovePlayerChip;

        chipSpawnerPresenter_Bot.OnSpawnChip -= chipCounterPresenter_Bot.AddChip;
        chipSpawnerPresenter_Player.OnSpawnChip -= chipCounterPresenter_Player.AddChip;
        chipSpawnerPresenter_Bot.OnDestroyChip -= chipCounterPresenter_Bot.RemoveChip;
        chipSpawnerPresenter_Player.OnDestroyChip -= chipCounterPresenter_Player.RemoveChip;

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

        chipSpawnerPresenter_Bot.OnPunch -= chipPunchPresenter.AddPunchChip;
        chipSpawnerPresenter_Player.OnPunch -= chipPunchPresenter.AddPunchChip;
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

        tutorialDescriptionPresenter.Dispose();

        storeStrategyPresenter.Dispose();

        spinMotionPresenter?.Dispose();
        fencePresenter?.Dispose();

        chipBotMovePresenter?.Dispose();
        chipMovePresenter?.Dispose();
        chipSpawnerPresenter_Player?.Dispose();
        chipSpawnerPresenter_Bot?.Dispose();
        storeChipPresenter.Dispose();
        botStoreChipPresenter?.Dispose();

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
