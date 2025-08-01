using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrategyPresentation_Game : IState
{
    private IGlobalStateMachine stateMachine;
    private UIGameRoot sceneRoot;
    private StoreStrategyPresenter storeStrategyPresenter;
    private StrategyBuyPresenter strategyBuyPresenter;
    private IParticleEffectProvider particleEffectProvider;
    private ISoundProvider soundProvider;

    private IEnumerator coroutineTimer;

    public StrategyPresentation_Game(IGlobalStateMachine stateMachine, UIGameRoot sceneRoot, StrategyBuyPresenter strategyBuyPresenter, StoreStrategyPresenter storeStrategyPresenter, IParticleEffectProvider particleEffectProvider, ISoundProvider soundProvider)
    {
        this.stateMachine = stateMachine;
        this.sceneRoot = sceneRoot;
        this.strategyBuyPresenter = strategyBuyPresenter;
        this.storeStrategyPresenter = storeStrategyPresenter;
        this.particleEffectProvider = particleEffectProvider;
        this.soundProvider = soundProvider;
    }

    public void EnterState()
    {
        strategyBuyPresenter.OnBuyStrategy += storeStrategyPresenter.OpenStrategy;

        sceneRoot.OpenStrategyPresentationPanel();
        particleEffectProvider.Play("NewStrategy");
        soundProvider.PlayOneShot("NewItem");

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
        stateMachine.SetState(stateMachine.GetState<BuyStrategy_Game>());
    }
}
