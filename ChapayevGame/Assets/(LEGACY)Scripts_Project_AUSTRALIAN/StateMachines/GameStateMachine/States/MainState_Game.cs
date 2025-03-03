using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainState_Game : IState
{
    private UIMiniGameSceneRoot sceneRoot;
    private CardColumnPresenter cardColumnPresenter;

    private StoreCardPresenter storeCardPresenter;
    private ScorePresenter scorePresenter;
    private MotionCounterPresenter motionCounterPresenter;
    private CardMotionHistoryPresenter cardMotionHistoryPresenter;
    private TimerPresenter timerPresenter;
    private MotionHintPresenter motionHintPresenter;

    private IGlobalStateMachine stateMachine;

    public MainState_Game(
        IGlobalStateMachine stateMachine, 
        UIMiniGameSceneRoot sceneRoot, 
        CardColumnPresenter cardColumnPresenter, 
        StoreCardPresenter storeCardPresenter, 
        ScorePresenter scorePresenter, 
        MotionCounterPresenter motionCounterPresenter, 
        CardMotionHistoryPresenter cardMotionHistoryPresenter, 
        TimerPresenter timerPresenter, 
        MotionHintPresenter motionHintPresenter)
    {
        this.stateMachine = stateMachine;
        this.sceneRoot = sceneRoot;
        this.cardColumnPresenter = cardColumnPresenter;
        this.storeCardPresenter = storeCardPresenter;
        this.scorePresenter = scorePresenter;
        this.motionCounterPresenter = motionCounterPresenter;
        this.cardMotionHistoryPresenter = cardMotionHistoryPresenter;
        this.timerPresenter = timerPresenter;
        this.motionHintPresenter = motionHintPresenter;
    }

    public void EnterState()
    {
        Debug.Log("ACTIVATE STATE - MAIN");

        storeCardPresenter.OnDealCardsFromStock_Value += cardColumnPresenter.DealCardsFromStock;
        cardColumnPresenter.OnCardDrop += scorePresenter.RemoveScoreByCardDrop;
        cardColumnPresenter.OnFullCompleteCardGroup += scorePresenter.AddScoreByFullComplect;
        cardColumnPresenter.OnCardDrop += motionCounterPresenter.AddMotion;
        cardColumnPresenter.OnCardDrop_Value += cardMotionHistoryPresenter.AddMotion;
        cardMotionHistoryPresenter.OnRemoveLastMotion += cardColumnPresenter.ReturnLastMotion;
        cardColumnPresenter.OnFullCompleteCardGroup += cardMotionHistoryPresenter.CleanHistory;
        storeCardPresenter.OnDealCardsFromStock += cardMotionHistoryPresenter.CleanHistory;
        cardColumnPresenter.OnFullCompleteCardGroup_Value += storeCardPresenter.DestroyCards;
        motionHintPresenter.OnMotionHint += cardColumnPresenter.MotionHint;

        sceneRoot.OnClickToRestart_Header += ChangeStateToRestart;
        sceneRoot.OnClickToExit_Header += ChangeStateToExit;
        cardColumnPresenter.OnWinning += ChangeStateToWin;

        sceneRoot.OpenHeaderPanel();

        timerPresenter.ResumeTimer();

    }

    public void ExitState()
    {
        Debug.Log("DEACTIVATE STATE - MAIN");

        storeCardPresenter.OnDealCardsFromStock_Value -= cardColumnPresenter.DealCardsFromStock;
        cardColumnPresenter.OnCardDrop -= scorePresenter.RemoveScoreByCardDrop;
        cardColumnPresenter.OnFullCompleteCardGroup -= scorePresenter.AddScoreByFullComplect;
        cardColumnPresenter.OnCardDrop -= motionCounterPresenter.AddMotion;
        cardColumnPresenter.OnCardDrop_Value -= cardMotionHistoryPresenter.AddMotion;
        cardMotionHistoryPresenter.OnRemoveLastMotion -= cardColumnPresenter.ReturnLastMotion;
        cardColumnPresenter.OnFullCompleteCardGroup -= cardMotionHistoryPresenter.CleanHistory;
        storeCardPresenter.OnDealCardsFromStock -= cardMotionHistoryPresenter.CleanHistory;
        cardColumnPresenter.OnFullCompleteCardGroup_Value -= storeCardPresenter.DestroyCards;
        motionHintPresenter.OnMotionHint -= cardColumnPresenter.MotionHint;

        sceneRoot.OnClickToRestart_Header -= ChangeStateToRestart;
        sceneRoot.OnClickToExit_Header -= ChangeStateToExit;
        cardColumnPresenter.OnWinning -= ChangeStateToWin;
    }

    private void ChangeStateToRestart()
    {
        stateMachine.SetState(stateMachine.GetState<RestartState_Game>());
    }

    private void ChangeStateToExit()
    {
        stateMachine.SetState(stateMachine.GetState<ExitState_Game>());
    }

    private void ChangeStateToWin()
    {
        stateMachine.SetState(stateMachine.GetState<WinState_Game>());
    }
}
