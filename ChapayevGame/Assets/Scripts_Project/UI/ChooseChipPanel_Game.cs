using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseChipPanel_Game : MovablePanel
{
    [SerializeField] private Button buttonCancel;
    [SerializeField] private Button buttonPlay;

    private ISoundProvider soundProvider;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public override void Initialize()
    {
        base.Initialize();

        buttonCancel.onClick.AddListener(HandleClickToCancel);
        buttonPlay.onClick.AddListener(HandleClickToPlay);
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonCancel.onClick.RemoveListener(HandleClickToCancel);
        buttonPlay.onClick.RemoveListener(HandleClickToPlay);
    }

    #region Input

    public event Action OnClickToCancel;
    public event Action OnClickToPlay;

    private void HandleClickToCancel()
    {
        soundProvider.PlayOneShot("Click");

        OnClickToCancel?.Invoke();
    }

    private void HandleClickToPlay()
    {
        soundProvider.PlayOneShot("Click");

        OnClickToPlay?.Invoke();
    }

    #endregion
}
