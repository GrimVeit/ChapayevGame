using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinPanel_Game : MovablePanel
{
    [SerializeField] private Button buttonChipStore;
    [SerializeField] private Button buttonStrategyStore;
    [SerializeField] private Button buttonChooseStrategy;

    private ISoundProvider soundProvider;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public override void Initialize()
    {
        base.Initialize();

        buttonChipStore.onClick.AddListener(HandleClickToChipStore);
        buttonStrategyStore.onClick.AddListener(HandleClickToStrategyStore);
        buttonChooseStrategy.onClick.AddListener(HandleClickToChooseStrategy);
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonChipStore.onClick.RemoveListener(HandleClickToChipStore);
        buttonStrategyStore.onClick.RemoveListener(HandleClickToStrategyStore);
        buttonChooseStrategy.onClick.RemoveListener(HandleClickToChooseStrategy);
    }

    #region Input

    public event Action OnClickToOpenChipStore;
    public event Action OnClickToOpenStrategyStore;
    public event Action OnClickToChooseStrategy;

    private void HandleClickToChipStore()
    {
        soundProvider.PlayOneShot("Click");

        OnClickToOpenChipStore?.Invoke();
    }

    private void HandleClickToStrategyStore()
    {
        soundProvider.PlayOneShot("Click");

        OnClickToOpenStrategyStore?.Invoke();
    }

    private void HandleClickToChooseStrategy()
    {
        soundProvider.PlayOneShot("Click");

        OnClickToChooseStrategy.Invoke();
    }

    #endregion
}
