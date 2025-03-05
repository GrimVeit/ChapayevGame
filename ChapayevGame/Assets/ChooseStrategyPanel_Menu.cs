using System;
using UnityEngine;
using UnityEngine.UI;

public class ChooseStrategyPanel_Menu : MovePanel
{
    [SerializeField] private Button buttonCancel;
    [SerializeField] private Button buttonContinue;

    public override void Initialize()
    {
        base.Initialize();

        buttonCancel.onClick.AddListener(() => OnClickToCancel?.Invoke());
        buttonContinue.onClick.AddListener(() => OnClickToContinue?.Invoke());
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonCancel.onClick.RemoveListener(() => OnClickToCancel?.Invoke());
        buttonContinue.onClick.RemoveListener(() => OnClickToContinue?.Invoke());
    }

    #region Input

    public event Action OnClickToCancel;
    public event Action OnClickToContinue;

    #endregion
}
