using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LosePanel_Game : MovePanel
{
    [SerializeField] private Button buttonChipStore;
    [SerializeField] private Button buttonStrategyStore;
    [SerializeField] private Button buttonChooseStrategy;

    public override void Initialize()
    {
        base.Initialize();

        buttonChipStore.onClick.AddListener(()=> OnClickToOpenChipStore?.Invoke());
        buttonStrategyStore.onClick.AddListener(()=> OnClickToOpenStrategyStore?.Invoke());
        buttonChooseStrategy.onClick.AddListener(()=> OnClickToChooseStrategy?.Invoke());
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonChipStore.onClick.RemoveListener(() => OnClickToOpenChipStore?.Invoke());
        buttonStrategyStore.onClick.RemoveListener(() => OnClickToOpenStrategyStore?.Invoke());
        buttonChooseStrategy.onClick.RemoveListener(() => OnClickToChooseStrategy?.Invoke());
    }

    #region Input

    public event Action OnClickToOpenChipStore;
    public event Action OnClickToOpenStrategyStore;
    public event Action OnClickToChooseStrategy;

    #endregion
}
