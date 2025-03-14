using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipPresentation_Menu : IState
{
    private IGlobalStateMachine stateMachine;
    private UIMainMenuRoot sceneRoot;
    private StoreChipPresenter storeChipPresenter;
    private ChipBuyPresenter chipBuyPresenter;

    private IEnumerator coroutineTimer;
    
    public ChipPresentation_Menu(IGlobalStateMachine stateMachine, UIMainMenuRoot sceneRoot, ChipBuyPresenter strategyBuyPresenter, StoreChipPresenter storeChipPresenter)
    {
        this.stateMachine = stateMachine;
        this.sceneRoot = sceneRoot;
        this.chipBuyPresenter = strategyBuyPresenter;
        this.storeChipPresenter = storeChipPresenter;
    }

    public void EnterState()
    {
        chipBuyPresenter.OnBuyChip += storeChipPresenter.OpenChip;

        sceneRoot.OpenChipPresentationPanel();

        if (coroutineTimer != null)
            Coroutines.Stop(coroutineTimer);

        coroutineTimer = Timer(3);
        Coroutines.Start(coroutineTimer);
    }

    public void ExitState()
    {
        chipBuyPresenter.OnBuyChip -= storeChipPresenter.OpenChip;


        sceneRoot.CloseChipPresentationPanel();
    }

    private IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(0.1f);

        chipBuyPresenter.Buy();

        yield return new WaitForSeconds(time);

        ChangeStateToChipPresentation();
    }

    private void ChangeStateToChipPresentation()
    {
        stateMachine.SetState(stateMachine.GetState<BuyChip_Menu>());
    }
}
