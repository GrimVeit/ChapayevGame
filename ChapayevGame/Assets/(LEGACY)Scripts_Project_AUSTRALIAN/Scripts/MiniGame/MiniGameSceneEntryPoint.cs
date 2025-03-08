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
    private ChipSpawnerPresenter chipSpawnerPresenter_Player;
    private ChipSpawnerPresenter chipSpawnerPresenter_Bot;
    private ChipMovePresenter chipMovePresenter;

    private FencePresenter fencePresenter;

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
        chipSpawnerPresenter_Player = new ChipSpawnerPresenter(new ChipSpawnerModel(), viewContainer.GetView<ChipSpawnerView>("Player"));
        chipSpawnerPresenter_Bot = new ChipSpawnerPresenter(new ChipSpawnerModel(), viewContainer.GetView<ChipSpawnerView>("Bot"));
        chipMovePresenter = new ChipMovePresenter(new ChipMoveModel(), viewContainer.GetView<ChipMoveView>());

        fencePresenter = new FencePresenter(new FenceModel(), viewContainer.GetView<FenceView>());

        stateMachine = new GameStateMachine();

        ActivateEvents();

        fencePresenter.Initialize();

        storeStrategyPresenter.Initialize();

        chipMovePresenter.Initialize();
        chipSpawnerPresenter_Player.Initialize();
        chipSpawnerPresenter_Bot.Initialize();
        storeChipPresenter.Initialize();

        stateMachine.Initialize();

        fencePresenter.RandomFence();
    }

    private void ActivateEvents()
    {
        ActivateTransitionEvents();

        storeStrategyPresenter.OnSelectStrategy += chipSpawnerPresenter_Player.SetStrategy;
        storeChipPresenter.OnSelectChip += chipSpawnerPresenter_Player.SetChip;

        storeStrategyPresenter.OnSelectStrategy += chipSpawnerPresenter_Bot.SetStrategy;
        storeChipPresenter.OnSelectChip += chipSpawnerPresenter_Bot.SetChip;

        chipSpawnerPresenter_Player.OnSpawnChip += chipMovePresenter.AddChip;
        chipSpawnerPresenter_Player.OnDestroyChip += chipMovePresenter.RemoveChip;
    }

    private void DeactivateEvents()
    {
        DeactivateTransitionEvents();

        storeStrategyPresenter.OnSelectStrategy -= chipSpawnerPresenter_Player.SetStrategy;
        storeChipPresenter.OnSelectChip -= chipSpawnerPresenter_Player.SetChip;

        storeStrategyPresenter.OnSelectStrategy -= chipSpawnerPresenter_Bot.SetStrategy;
        storeChipPresenter.OnSelectChip -= chipSpawnerPresenter_Bot.SetChip;

        chipSpawnerPresenter_Player.OnSpawnChip -= chipMovePresenter.AddChip;
        chipSpawnerPresenter_Player.OnDestroyChip -= chipMovePresenter.RemoveChip;
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

        fencePresenter?.Dispose();

        chipMovePresenter?.Dispose();
        chipSpawnerPresenter_Player?.Dispose();
        chipSpawnerPresenter_Bot?.Dispose();
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
