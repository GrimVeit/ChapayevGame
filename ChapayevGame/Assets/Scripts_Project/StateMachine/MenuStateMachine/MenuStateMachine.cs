using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuStateMachine : IGlobalStateMachine
{
    private Dictionary<Type, IState> states = new Dictionary<Type, IState>();

    private IState currentState;

    public MenuStateMachine(UIMainMenuRoot sceneRoot)
    {
        states[typeof(MainState_Menu)] = new MainState_Menu(this, sceneRoot);
    }

    public void Initialize()
    {

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
