using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel_Menu : MovePanel
{
    [SerializeField] private Button buttonBattle;
    [SerializeField] private Button buttonStrategy;
    [SerializeField] private Button buttonCollection;

    public override void Initialize()
    {
        base.Initialize();

        buttonBattle.onClick.AddListener(()=> OnClickToBattle?.Invoke());
        buttonStrategy.onClick.AddListener(()=> OnClickToStrategy?.Invoke());
        buttonCollection.onClick.AddListener(()=> OnClickToCollection?.Invoke());
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonBattle.onClick.RemoveListener(() => OnClickToBattle?.Invoke());
        buttonStrategy.onClick.RemoveListener(() => OnClickToStrategy?.Invoke());
        buttonCollection.onClick.RemoveListener(() => OnClickToCollection?.Invoke());
    }

    #region Input

    public event Action OnClickToBattle;
    public event Action OnClickToStrategy;
    public event Action OnClickToCollection;

    #endregion
}
