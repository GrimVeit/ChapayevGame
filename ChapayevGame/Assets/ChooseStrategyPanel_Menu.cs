using System;
using UnityEngine;
using UnityEngine.UI;

public class ChooseStrategyPanel_Menu : MovePanel
{
    [SerializeField] private Button buttonCancel;
    [SerializeField] private Button buttonContinue;

    private ISoundProvider soundProvider;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public override void Initialize()
    {
        base.Initialize();

        buttonCancel.onClick.AddListener(HandleClicKToCancel);
        buttonContinue.onClick.AddListener(HandleClicKToContinue);
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonCancel.onClick.RemoveListener(HandleClicKToCancel);
        buttonContinue.onClick.RemoveListener(HandleClicKToContinue);
    }

    #region Input

    public event Action OnClickToCancel;
    public event Action OnClickToContinue;

    private void HandleClicKToCancel()
    {
        soundProvider.PlayOneShot("Click");

        OnClickToCancel?.Invoke();
    }

    private void HandleClicKToContinue()
    {
        soundProvider.PlayOneShot("Click");

        OnClickToContinue?.Invoke();
    }

    #endregion
}
