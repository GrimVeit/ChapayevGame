using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuStateMachine : IGlobalStateMachine
{
    private Dictionary<Type, IState> states = new Dictionary<Type, IState>();

    private IState currentState;

    public MenuStateMachine(
        UIMainMenuRoot sceneRoot,
        StoreStrategyPresenter storeStrategyPresenter,
        StrategyBuyPresenter strategyBuyPresenter,
        StrategyBuyVisualizePresenter strategyBuyVisualizePresenter)
    {
        states[typeof(MainState_Menu)] = new MainState_Menu(this, sceneRoot);
        states[typeof(BuyStrategy_Menu)] = new BuyStrategy_Menu(this, sceneRoot, strategyBuyPresenter, storeStrategyPresenter, strategyBuyVisualizePresenter);
    }

    public void Initialize()
    {
        SetState(GetState<MainState_Menu>());
    }

    public void Dispose()
    {

    }

    public IState GetState<T>() where T : IState
    {
        return states[typeof(T)];
    }

    public void SetState(IState state)
    {
        currentState?.ExitState();

        currentState = state;
        currentState.EnterState();
    }
}
