using System;
using UnityEngine;

public class MiniGameSceneEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private StrategyGroup strategyGroup;
    [SerializeField] private ChipGroup chipGroup;
    [SerializeField] private UIMiniGameSceneRoot sceneRootPrefab;

    private UIMiniGameSceneRoot sceneRoot;
    private ViewContainer viewContainer;

    private StoreStrategyPresenter storeStrategyPresenter;

    private StoreChipPresenter storeChipPresenter;
    private ChipSpawnerPresenter chipSpawnerPresenter;
    private ChipMovePresenter chipMovePresenter;

    private GameStateMachine stateMachine;

    public void Run(UIRootView uIRootView)
    {
        sceneRoot = sceneRootPrefab;

        uIRootView.AttachSceneUI(sceneRoot.gameObject, Camera.main);

        sceneRoot.Activate();

        viewContainer = sceneRoot.GetComponent<ViewContainer>();
        viewContainer.Initialize();

        storeStrategyPresenter = new StoreStrategyPresenter(new StoreStrategyModel(strategyGroup));

        storeChipPresenter = new StoreChipPresenter(new StoreChipModel(chipGroup));
        chipSpawnerPresenter = new ChipSpawnerPresenter(new ChipSpawnerModel(), viewContainer.GetView<ChipSpawnerView>());
        chipMovePresenter = new ChipMovePresenter(new ChipMoveModel(), viewContainer.GetView<ChipMoveView>());

        stateMachine = new GameStateMachine();

        ActivateEvents();

        storeStrategyPresenter.Initialize();

        chipMovePresenter.Initialize();
        chipSpawnerPresenter.Initialize();
        storeChipPresenter.Initialize();

        stateMachine.Initialize();
    }

    private void ActivateEvents()
    {
        ActivateTransitionEvents();

        storeStrategyPresenter.OnSelectStrategy += chipSpawnerPresenter.SetStrategy;
        storeChipPresenter.OnSelectChip += chipSpawnerPresenter.SetChip;
    }

    private void DeactivateEvents()
    {
        DeactivateTransitionEvents();

        storeStrategyPresenter.OnSelectStrategy -= chipSpawnerPresenter.SetStrategy;
        storeChipPresenter.OnSelectChip -= chipSpawnerPresenter.SetChip;
    }

    private void ActivateTransitionEvents()
    {

    }

    private void DeactivateTransitionEvents()
    {

    }

    public void Dispose()
    {
        DeactivateEvents();

        sceneRoot?.Dispose();

        //storeStrategyPresenter.Dispose();

        chipMovePresenter?.Dispose();
        chipSpawnerPresenter?.Dispose();
        //storeChipPresenter.Dispose();

        stateMachine?.Dispose();
    }

    private void OnDestroy()
    {
        Dispose();
    }

    #region Input

    public event Action OnGoToMainMenu;
    public event Action OnGoToGame;

    private void HandleGoToMainMenu()
    {
        sceneRoot.Deactivate();
        OnGoToMainMenu?.Invoke();
    }

    private void HandleGoToGame()
    {
        sceneRoot.Deactivate();
        OnGoToGame?.Invoke();
    }

    #endregion
}
