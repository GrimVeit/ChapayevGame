using System;
using System.Collections.Generic;

public class GameStateMachine : IGlobalStateMachine
{
    private Dictionary<Type, IState> states = new Dictionary<Type, IState>();

    private IState currentState;

    public GameStateMachine(
        UIMiniGameSceneRoot sceneRoot, 
        SpinMotionPresenter spinMotionPresenter, 
        ChipBotMovePresenter chipBotMovePresenter,
        ChipMovePresenter chipMovePresenter, 
        GameResultPresenter gameResultPresenter,
        
        StoreChipPresenter storeChipPresenter,
        ChipBuyVisualizePresenter chipBuyVisualizePresenter,
        ChipBuyPresenter chipBuyPresenter,
        ChipSelectPresenter chipSelectPresenter,
        
        StoreStrategyPresenter storeStrategyPresenter,
        StrategyBuyVisualizePresenter strategyBuyVisualizePresenter,
        StrategyBuyPresenter strategyBuyPresenter,
        StrategySelectPresenter strategySelectPresenter,
        
        ChipSpawnerPresenter chipSpawnerPresenter_Player,
        ChipSpawnerPresenter chipSpawnerPresenter_Bot,
        
        ITutorialDescriptionProvider tutorialDescriptionProvider)
    {
        states[typeof(SpinStartState_Game)] = new SpinStartState_Game(this, sceneRoot, chipMovePresenter);
        states[typeof(SpinState_Game)] = new SpinState_Game(this, sceneRoot, spinMotionPresenter, tutorialDescriptionProvider);
        states[typeof(PlayerMotionState_Game)] = new PlayerMotionState_Game(this, chipMovePresenter, gameResultPresenter, tutorialDescriptionProvider);
        states[typeof(FromPlayerMotionToBotMotion_Game)] = new FromPlayerMotionToBotMotion_Game(this, gameResultPresenter, chipMovePresenter);
        states[typeof(BotMotionState_Game)] = new BotMotionState_Game(this, gameResultPresenter, chipBotMovePresenter);
        states[typeof(FromBotMotionToPlayerMotion_Game)] = new FromBotMotionToPlayerMotion_Game(this, gameResultPresenter, chipBotMovePresenter);
        states[typeof(WinState_Game)] = new WinState_Game(this, sceneRoot, storeChipPresenter, storeStrategyPresenter, chipSpawnerPresenter_Player, chipSpawnerPresenter_Bot);
        states[typeof(LoseState_Game)] = new LoseState_Game(this, sceneRoot, storeStrategyPresenter, storeChipPresenter, chipSpawnerPresenter_Player, chipSpawnerPresenter_Bot);

        states[typeof(BuyChip_Game)] = new BuyChip_Game(this, sceneRoot, chipBuyPresenter, storeChipPresenter, chipBuyVisualizePresenter, gameResultPresenter);
        states[typeof(BuyStrategy_Game)] = new BuyStrategy_Game(this, sceneRoot, strategyBuyPresenter, storeStrategyPresenter, strategyBuyVisualizePresenter, gameResultPresenter);
        states[typeof(ChooseChip_Game)] = new ChooseChip_Game(this, sceneRoot, storeChipPresenter, chipSelectPresenter);
        states[typeof(ChooseStrategy_Game)] = new ChooseStrategy_Game(this, sceneRoot, storeStrategyPresenter, strategySelectPresenter, storeChipPresenter, gameResultPresenter);
    }

    public void Initialize()
    {
        SetState(GetState<SpinStartState_Game>());
    }

    public void Dispose()
    {

    }

    public void SetState(IState state)
    {
        currentState?.ExitState();

        currentState = state;
        currentState.EnterState();
    }

    public IState GetState<T>() where T : IState
    {
        return states[typeof(T)];
    }
}

public interface IGlobalStateMachine
{
    public void SetState(IState state);

    public IState GetState<T>() where T : IState;
}
