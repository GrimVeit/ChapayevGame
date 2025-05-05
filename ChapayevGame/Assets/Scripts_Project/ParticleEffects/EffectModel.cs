using System;
using System.Collections.Generic;
using UnityEngine;

public class EffectModel
{
    public event Action OnActivateEffect;
    
    public Dictionary<string, Effect> particleEffects = new Dictionary<string, Effect>();

    public void Initialize(Effect[] effects)
    {
        for (int i = 0; i < effects.Length; i++)
        {
            particleEffects.Add(effects[i].ID, effects[i]);
            effects[i].Initialize();
        }
    }

    public void Dispose()
    {
        foreach (var item in particleEffects.Values)
        {
            item.Dispose();
        }
    }

    public void Play(string ID)
    {
        if (particleEffects.ContainsKey(ID))
        {
            OnActivateEffect?.Invoke();
            particleEffects[ID].Play();
        }
    }

    public IParticleEffect GetParticleEffect(string id)
    {
        if (particleEffects.ContainsKey(id))
        {
            return particleEffects[id];
        }

        Debug.Log("Ёффект с идентификатором " + id + "не был найден");
        return null;
    }
}
