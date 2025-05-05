using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPresenter : ISoundProvider
{
    private AudioModel soundModel;
    private AudioView soundView;

    public AudioPresenter(AudioModel soundModel, AudioView soundView)
    {
        this.soundModel = soundModel;
        this.soundView = soundView;
    }

    public void Initialize()
    {
        ActivateEvents();

        soundModel.Initialize();
        soundView.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        soundModel.Dispose();
        soundView.Dispose();
    }

    private void ActivateEvents()
    {
        soundView.OnClickSoundButton += soundModel.MuteUnmute;
    }

    private void DeactivateEvents()
    {
        soundView.OnClickSoundButton -= soundModel.MuteUnmute;
    }

    #region Interface

    public void Play(string id)
    {
        soundModel.Play(id);
    }

    public void PlayOneShot(string id)
    {
        soundModel.PlayOneShot(id);
    }

    public ISound GetSound(string id)
    {
        return soundModel.GetSound(id);
    }

    #endregion
}

public interface ISoundProvider
{
    void Play(string id);
    void PlayOneShot(string id);
    ISound GetSound(string id);
}
