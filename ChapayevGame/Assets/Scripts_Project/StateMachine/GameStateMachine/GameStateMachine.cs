using System;
using System.Collections.Generic;

public class GameStateMachine : IGlobalStateMachine
{
    private Dictionary<Type, IState> states = new Dictionary<Type, IState>();

    private IState currentState;

    public GameStateMachine(UIMiniGameSceneRoot sceneRoot, SpinMotionPresenter spinMotionPresenter, ChipMovePresenter chipMovePresenter, GameResultPresenter gameResultPresenter)
    {
        states[typeof(SpinStartState_Game)] = new SpinStartState_Game(this, sceneRoot, chipMovePresenter);
        states[typeof(SpinState_Game)] = new SpinState_Game(this, sceneRoot, spinMotionPresenter);
        states[typeof(PlayerMotionState_Game)] = new PlayerMotionState_Game(this, chipMovePresenter, gameResultPresenter);
        states[typeof(BotMotionState_Game)] = new BotMotionState_Game(this, gameResultPresenter);
        states[typeof(WinState_Game)] = new WinState_Game(sceneRoot);
        states[typeof(LoseState_Game)] = new LoseState_Game(sceneRoot);
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
