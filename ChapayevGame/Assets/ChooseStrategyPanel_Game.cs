using System;
using UnityEngine;
using UnityEngine.UI;

public class ChooseStrategyPanel_Game : MovePanel
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

        buttonCancel.onClick.AddListener(HandleClickToCancel);
        buttonContinue.onClick.AddListener(HandleClickToContinue);
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonCancel.onClick.RemoveListener(HandleClickToCancel);
        buttonContinue.onClick.RemoveListener(HandleClickToContinue);
    }

    #region Input

    public event Action OnClickToCancel;
    public event Action OnClickToContinue;

    private void HandleClickToCancel()
    {
        soundProvider.PlayOneShot("Click");

        OnClickToCancel?.Invoke();
    }

    private void HandleClickToContinue()
    {
        soundProvider.PlayOneShot("Click");

        OnClickToContinue?.Invoke();
    }

    #endregion
}
