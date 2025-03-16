using System;
using UnityEngine;

public class MainMenuEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private StrategyGroup strategyGroup;
    [SerializeField] private ChipGroup chipGroup;
    [SerializeField] private TutorialDescriptionGroup tutorialDescriptionGroup;
    [SerializeField] private UIMainMenuRoot menuRootPrefab;

    private UIMainMenuRoot sceneRoot;
    private ViewContainer viewContainer;

    private BankPresenter bankPresenter;
    private ParticleEffectPresenter particleEffectPresenter;
    private SoundPresenter soundPresenter;

    private StoreStrategyPresenter storeStrategyPresenter;
    private StrategyBuyVisualizePresenter strategyBuyVisualizePresenter;
    private StrategyBuyPresenter strategyBuyPresenter;
    private StrategySelectPresenter strategySelectPresenter;

    private StoreChipPresenter storeChipPresenter;
    private ChipBuyVisualizePresenter chipBuyVisualizePresenter;
    private ChipBuyPresenter chipBuyPresenter;
    private ChipSelectPresenter chipSelectPresenter;

    private TutorialDescriptionPresenter tutorialDescriptionPresenter;

    private AnimationFramePresenter animationFramePresenter;

    private MenuStateMachine stateMachine;

    public void Run(UIRootView uIRootView)
    {
        sceneRoot = menuRootPrefab;
 
        uIRootView.AttachSceneUI(sceneRoot.gameObject, Camera.main);

        viewContainer = sceneRoot.GetComponent<ViewContainer>();
        viewContainer.Initialize();

        soundPresenter = new SoundPresenter
            (new SoundModel(sounds.sounds, PlayerPrefsKeys.IS_MUTE_SOUNDS),
            viewContainer.GetView<SoundView>());

        particleEffectPresenter = new ParticleEffectPresenter
            (new ParticleEffectModel(),
            viewContainer.GetView<ParticleEffectView>());

        bankPresenter = new BankPresenter(new BankModel(), viewContainer.GetView<BankView>());

        sceneRoot.SetSoundProvider(soundPresenter);
        sceneRoot.Activate();

        animationFramePresenter = new AnimationFramePresenter(new AnimationFrameModel(), viewContainer.GetView<AnimationFrameView>());
        tutorialDescriptionPresenter = new TutorialDescriptionPresenter(new TutorialDescriptionModel(tutorialDescriptionGroup), viewContainer.GetView<TutorialDescriptionView>());

        storeStrategyPresenter = new StoreStrategyPresenter(new StoreStrategyModel(strategyGroup));
        strategyBuyPresenter = new StrategyBuyPresenter(new StrategyBuyModel(bankPresenter, storeStrategyPresenter), viewContainer.GetView<StrategyBuyView>());
        strategyBuyVisualizePresenter = new StrategyBuyVisualizePresenter(new StrategyBuyVisualizeModel(), viewContainer.GetView<StrategyBuyVisualizeView>());
        strategySelectPresenter = new StrategySelectPresenter(new StrategySelectModel(tutorialDescriptionPresenter), viewContainer.GetView<StrategySelectView>());

        storeChipPresenter = new StoreChipPresenter(new StoreChipModel(chipGroup));
        chipBuyPresenter = new ChipBuyPresenter(new ChipBuyModel(bankPresenter, storeChipPresenter), viewContainer.GetView<ChipBuyView>());
        chipBuyVisualizePresenter = new ChipBuyVisualizePresenter(new ChipBuyVisualizeModel(), viewContainer.GetView<ChipBuyVisualizeView>());
        chipSelectPresenter = new ChipSelectPresenter(new ChipSelectModel(tutorialDescriptionPresenter), viewContainer.GetView<ChipSelectView>());

        stateMachine = new MenuStateMachine(
            sceneRoot, 
            storeStrategyPresenter, 
            strategyBuyPresenter, 
            strategyBuyVisualizePresenter, 
            strategySelectPresenter,
            storeChipPresenter,
            chipBuyPresenter,
            chipBuyVisualizePresenter,
            chipSelectPresenter,
            tutorialDescriptionPresenter,
            animationFramePresenter,
            particleEffectPresenter);

        storeStrategyPresenter.UnselectAllStrategies();
        storeChipPresenter.UnselectAllChips();

        ActivateEvents();

        animationFramePresenter.Initialize();
        tutorialDescriptionPresenter.Initialize();

        soundPresenter.Initialize();
        particleEffectPresenter.Initialize();
        sceneRoot.Initialize();
        bankPresenter.Initialize();

        strategySelectPresenter.Initialize();
        strategyBuyVisualizePresenter.Initialize();
        strategyBuyPresenter.Initialize();
        storeStrategyPresenter.Initialize();

        chipSelectPresenter.Initialize();
        chipBuyVisualizePresenter.Initialize();
        chipBuyPresenter.Initialize();
        storeChipPresenter.Initialize();

        stateMachine.Initialize();

    }

    private void ActivateEvents()
    {
        ActivateTransitionsSceneEvents();

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
    }

    private void DeactivateEvents()
    {
        DeactivateTransitionsSceneEvents();

        storeStrategyPresenter.OnOpenStrategy -= strategyBuyVisualizePresenter.SetOpenStrategy;
        storeStrategyPresenter.OnOpenNewStrategy -= strategyBuyVisualizePresenter.SetOpenNewStrategy;
        storeStrategyPresenter.OnCloseStrategy -= strategyBuyVisualizePresenter.SetCloseStrategy;
        storeStrategyPresenter.OnOpenStrategy -= strategySelectPresenter.SetOpenStrategy;
        storeStrategyPresenter.OnOpenNewStrategy -= strategySelectPresenter.SetOpenNewStrategy;
        storeStrategyPresenter.OnSelectStrategy -= strategySelectPresenter.SelectStrategy;
        storeStrategyPresenter.OnDeselectStrategy -= strategySelectPresenter.DeselectStrategy;

        storeStrategyPresenter.OnSelectStrategy -= chipSelectPresenter.SetStrategy;
        storeChipPresenter.OnOpenChip -= chipBuyVisualizePresenter.SetOpenChip;
        storeChipPresenter.OnOpenNewChip -= chipBuyVisualizePresenter.SetOpenNewChip;
        storeChipPresenter.OnCloseChip -= chipBuyVisualizePresenter.SetCloseChip;
        storeChipPresenter.OnOpenChip -= chipSelectPresenter.SetOpenChip;
        storeChipPresenter.OnOpenNewChip -= chipSelectPresenter.SetOpenNewChip;
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

    private void Deactivate()
    {
        sceneRoot.Deactivate();
        soundPresenter?.Dispose();
    }

    private void Dispose()
    {
        DeactivateEvents();

        animationFramePresenter?.Dispose();
        tutorialDescriptionPresenter?.Dispose();

        soundPresenter?.Dispose();
        sceneRoot?.Dispose();
        particleEffectPresenter?.Dispose();
        bankPresenter?.Dispose();

        strategySelectPresenter?.Dispose();
        strategyBuyVisualizePresenter?.Dispose();
        strategyBuyPresenter?.Dispose();
        storeStrategyPresenter?.Dispose();

        chipSelectPresenter.Dispose();
        chipBuyVisualizePresenter?.Dispose();
        chipBuyPresenter?.Dispose();
        storeChipPresenter?.Dispose();

        stateMachine?.Dispose();
    }

    private void OnDestroy()
    {
        Dispose();
    }

    #region Input actions

    public event Action OnGoToGame;

    private void HandleGoToGame()
    {
        Deactivate();
        OnGoToGame?.Invoke();
    }

    #endregion
}
