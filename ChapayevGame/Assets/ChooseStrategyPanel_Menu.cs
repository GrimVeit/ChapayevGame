using System;
using UnityEngine;
using UnityEngine.UI;

public class ChooseStrategyPanel_Menu : MovePanel
{
    [SerializeField] private Button buttonCancel;

    public override void Initialize()
    {
        base.Initialize();

        buttonCancel.onClick.AddListener(() => OnClickToCancel?.Invoke());
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonCancel.onClick.RemoveListener(() => OnClickToCancel?.Invoke());
    }

    #region Input

    public event Action OnClickToCancel;

    #endregion
}
