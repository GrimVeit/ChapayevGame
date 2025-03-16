using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrategyPresentation_Menu : IState
{
    private IGlobalStateMachine stateMachine;
    private UIMainMenuRoot sceneRoot;
    private StoreStrategyPresenter storeStrategyPresenter;
    private StrategyBuyPresenter strategyBuyPresenter;
    private IParticleEffectProvider particleEffectProvider;

    private IEnumerator coroutineTimer;

    public StrategyPresentation_Menu(IGlobalStateMachine stateMachine, UIMainMenuRoot sceneRoot, StrategyBuyPresenter strategyBuyPresenter, StoreStrategyPresenter storeStrategyPresenter, IParticleEffectProvider particleEffectProvider)
    {
        this.stateMachine = stateMachine;
        this.sceneRoot = sceneRoot;
        this.strategyBuyPresenter = strategyBuyPresenter;
        this.storeStrategyPresenter = storeStrategyPresenter;
        this.particleEffectProvider = particleEffectProvider;
    }

    public void EnterState()
    {
        strategyBuyPresenter.OnBuyStrategy += storeStrategyPresenter.OpenStrategy;

        sceneRoot.OpenStrategyPresentationPanel();
        particleEffectProvider.Play("NewStrategy");

        if (coroutineTimer != null)
            Coroutines.Stop(coroutineTimer);

        coroutineTimer = Timer(1);
        Coroutines.Start(coroutineTimer);
    }

    public void ExitState()
    {
        strategyBuyPresenter.OnBuyStrategy -= storeStrategyPresenter.OpenStrategy;


        sceneRoot.CloseStrategyPresentationPanel();
    }

    private IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(1f);

        strategyBuyPresenter.Buy();

        yield return new WaitForSeconds(time);

        ChangeStateToStrategyPresentation();
    }

    private void ChangeStateToStrategyPresentation()
    {
        stateMachine.SetState(stateMachine.GetState<BuyStrategy_Menu>());
    }
}
