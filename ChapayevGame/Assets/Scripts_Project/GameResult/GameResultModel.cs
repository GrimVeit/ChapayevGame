using System;
using UnityEngine;

public class GameResultModel
{
    public event Action<int> OnWin_Value;
    public event Action OnWin;
    public event Action OnLose;

    private int countChipsPlayer = 0;
    private int countChipsBot = 0;

    private WinPrices winPrices;

    public GameResultModel(WinPrices winPrices)
    {
        this.winPrices = winPrices;
    }

    public void AddPlayerChip()
    {
        countChipsPlayer += 1;
    }

    public void RemovePlayerChip()
    {
        countChipsPlayer -= 1;

        CheckGameResult();
    }

    public void AddBotChip()
    {
        countChipsBot += 1;
    }

    public void RemoveBotChip()
    {
        countChipsBot -= 1;

        CheckGameResult();
    }

    private void CheckGameResult()
    {
        if(countChipsPlayer == 0)
        {
            OnLose?.Invoke();
            Debug.Log("LOSE GAME");
        }

        if(countChipsBot == 0)
        {
            OnWin_Value?.Invoke(winPrices.GetWinPriceByChipCount(countChipsPlayer).Win);
            OnWin?.Invoke();
            Debug.Log("WIN GAME - " + countChipsPlayer);
        }
    }
}
