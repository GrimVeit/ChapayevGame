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

    private ISoundProvider soundProvider;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public override void Initialize()
    {
        base.Initialize();

        buttonBattle.onClick.AddListener(HandleClickToBattle);
        buttonStrategy.onClick.AddListener(HandleClickToStrategy);
        buttonCollection.onClick.AddListener(HandleClickToCollection);
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonBattle.onClick.RemoveListener(HandleClickToBattle);
        buttonStrategy.onClick.RemoveListener(HandleClickToStrategy);
        buttonCollection.onClick.RemoveListener(HandleClickToCollection);
    }

    #region Input

    public event Action OnClickToBattle;
    public event Action OnClickToStrategy;
    public event Action OnClickToCollection;

    private void HandleClickToBattle()
    {
        soundProvider.PlayOneShot("Click");

        OnClickToBattle?.Invoke();
    }

    private void HandleClickToStrategy()
    {
        soundProvider.PlayOneShot("Click");

        OnClickToStrategy?.Invoke();
    }

    private void HandleClickToCollection()
    {
        soundProvider.PlayOneShot("Click");

        OnClickToCollection?.Invoke();
    }

    #endregion
}
