using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrategyPresentation_Game : IState
{
    private IGlobalStateMachine stateMachine;
    private UIMiniGameSceneRoot sceneRoot;
    private StoreStrategyPresenter storeStrategyPresenter;
    private StrategyBuyPresenter strategyBuyPresenter;

    private IEnumerator coroutineTimer;

    public StrategyPresentation_Game(IGlobalStateMachine stateMachine, UIMiniGameSceneRoot sceneRoot, StrategyBuyPresenter strategyBuyPresenter, StoreStrategyPresenter storeStrategyPresenter)
    {
        this.stateMachine = stateMachine;
        this.sceneRoot = sceneRoot;
        this.strategyBuyPresenter = strategyBuyPresenter;
        this.storeStrategyPresenter = storeStrategyPresenter;
    }

    public void EnterState()
    {
        strategyBuyPresenter.OnBuyStrategy += storeStrategyPresenter.OpenStrategy;

        sceneRoot.OpenStrategyPresentationPanel();

        if (coroutineTimer != null)
            Coroutines.Stop(coroutineTimer);

        coroutineTimer = Timer(3);
        Coroutines.Start(coroutineTimer);
    }

    public void ExitState()
    {
        strategyBuyPresenter.OnBuyStrategy -= storeStrategyPresenter.OpenStrategy;


        sceneRoot.CloseStrategyPresentationPanel();
    }

    private IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(0.1f);

        strategyBuyPresenter.Buy();

        yield return new WaitForSeconds(time);

        ChangeStateToStrategyPresentation();
    }

    private void ChangeStateToStrategyPresentation()
    {
        stateMachine.SetState(stateMachine.GetState<BuyStrategy_Game>());
    }
}
