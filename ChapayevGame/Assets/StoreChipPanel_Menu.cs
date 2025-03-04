using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreChipPanel_Menu : MovePanel
{
    [SerializeField] private Button buttonCancel;

    public override void Initialize()
    {
        base.Initialize();
    }

    public override void Dispose()
    {
        base.Dispose();
    }

    #region Input

    public event Action OnClickToCancel;

    #endregion
}
