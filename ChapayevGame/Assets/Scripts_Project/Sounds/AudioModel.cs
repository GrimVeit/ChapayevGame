using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioModel
{
    public event Action OnMuteSounds;
    public event Action OnUnmuteSounds;

    public Dictionary<string, Audio> sounds = new Dictionary<string, Audio>();

    private string KEY;
    private bool isMute = false;

    public AudioModel(List<Audio> sounds, string key)
    {
        KEY = key;

        for (int i = 0; i < sounds.Count; i++)
        {
            this.sounds[sounds[i].ID] = sounds[i];
        }
    }

    public void Initialize()
    {
        isMute = PlayerPrefs.GetInt(KEY, 1) == 0;

        foreach (var sound in sounds.Values)
        {
            sound.Initialize();
        }

        CheckMuteUnmute();
    }

    public void Dispose()
    {
        int value;

        if (isMute) value = 0;
        else value = 1;

        PlayerPrefs.SetInt(KEY, value);

        foreach (var sound in sounds.Values)
        {
            sound.Dispose();
        }
    }

    public void MuteUnmute()
    {
        isMute = !isMute;
        CheckMuteUnmute();
    }

    private void CheckMuteUnmute()
    {
        if (isMute)
        {
            MuteAll();
            OnMuteSounds?.Invoke();
        }
        else
        {
            UnmuteAll();
            OnUnmuteSounds?.Invoke();
        }
    }

    private void MuteAll()
    {
        foreach (var sound in sounds.Values)
        {
            sound.MainMute();
        }
    }

    private void UnmuteAll()
    {
        foreach (var sound in sounds.Values)
        {
            sound.MainUnmute();
        }
    }

    public ISound GetSound(string id)
    {
        if (sounds.ContainsKey(id))
        {
            return sounds[id];
        }

        Debug.LogError("��� ��������� ����� � ��������������� " + id);
        return null;
    }

    public void Play(string id)
    {
        if (sounds.ContainsKey(id))
        {
            sounds[id].Play();
            return;
        }

        Debug.LogError("��� ��������� ����� � ��������������� " + id);
    }

    public void PlayOneShot(string id)
    {
        if (sounds.ContainsKey(id))
        {
            sounds[id].PlayOneShot();
            return;
        }

        Debug.LogError("��� ��������� ����� � ��������������� " + id);
    }
}
