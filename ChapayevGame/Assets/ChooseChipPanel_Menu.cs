using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseChipPanel_Menu : MovePanel
{
    [SerializeField] private Button buttonCancel;
    [SerializeField] private Button buttonPlay;

    public override void Initialize()
    {
        base.Initialize();

        buttonCancel.onClick.AddListener(() => OnClickToCancel?.Invoke());
        buttonPlay.onClick.AddListener(() => OnClickToPlay?.Invoke());
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonCancel.onClick.RemoveListener(() => OnClickToCancel?.Invoke());
        buttonPlay.onClick.RemoveListener(() => OnClickToPlay?.Invoke());
    }

    #region Input

    public event Action OnClickToCancel;
    public event Action OnClickToPlay;

    #endregion
}
