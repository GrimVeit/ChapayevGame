using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipPresentation_Game : IState
{
    private IGlobalStateMachine stateMachine;
    private UIMiniGameSceneRoot sceneRoot;
    private StoreChipPresenter storeChipPresenter;
    private ChipBuyPresenter chipBuyPresenter;
    private IParticleEffectProvider particleEffectProvider;

    private IEnumerator coroutineTimer;

    public ChipPresentation_Game(IGlobalStateMachine stateMachine, UIMiniGameSceneRoot sceneRoot, ChipBuyPresenter strategyBuyPresenter, StoreChipPresenter storeChipPresenter, IParticleEffectProvider particleEffectProvider)
    {
        this.stateMachine = stateMachine;
        this.sceneRoot = sceneRoot;
        this.chipBuyPresenter = strategyBuyPresenter;
        this.storeChipPresenter = storeChipPresenter;
        this.particleEffectProvider = particleEffectProvider;
    }

    public void EnterState()
    {
        chipBuyPresenter.OnBuyChip += storeChipPresenter.OpenChip;

        sceneRoot.OpenChipPresentationPanel();
        particleEffectProvider.Play("NewChip");

        if (coroutineTimer != null)
            Coroutines.Stop(coroutineTimer);

        coroutineTimer = Timer(1f);
        Coroutines.Start(coroutineTimer);
    }

    public void ExitState()
    {
        chipBuyPresenter.OnBuyChip -= storeChipPresenter.OpenChip;


        sceneRoot.CloseChipPresentationPanel();
    }

    private IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(1f);

        chipBuyPresenter.Buy();

        yield return new WaitForSeconds(time);

        ChangeStateToChipPresentation();
    }

    private void ChangeStateToChipPresentation()
    {
        stateMachine.SetState(stateMachine.GetState<BuyChip_Game>());
    }
}
