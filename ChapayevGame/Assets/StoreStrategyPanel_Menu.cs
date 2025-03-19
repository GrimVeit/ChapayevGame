using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreStrategyPanel_Menu : MovePanel
{
    [SerializeField] private Button buttonCancel;

    private ISoundProvider soundProvider;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public override void Initialize()
    {
        base.Initialize();

        buttonCancel.onClick.AddListener(HandleClickToCancel);
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonCancel.onClick.RemoveListener(HandleClickToCancel);
    }

    #region Input

    public event Action OnClickToCancel;

    private void HandleClickToCancel()
    {
        soundProvider.PlayOneShot("Click");

        OnClickToCancel?.Invoke();
    }

    #endregion
}
