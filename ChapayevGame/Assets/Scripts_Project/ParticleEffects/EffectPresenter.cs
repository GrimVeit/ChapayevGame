using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPresenter : IParticleEffectProvider
{
    private EffectModel effectModel;
    private EffectView effectView;

    public EffectPresenter(EffectModel effectModel, EffectView effectView)
    {
        this.effectModel = effectModel;
        this.effectView = effectView;
    }

    public void Initialize()
    {
        effectModel.Initialize(effectView.particleEffects.effects.ToArray());
    }

    public void Dispose()
    {
        effectModel.Dispose();
    }

    public void Play(string ID)
    {
        effectModel.Play(ID);
    }

    public IParticleEffect GetParticleEffect(string ID)
    {
        return effectModel.GetParticleEffect(ID);
    }
}

public interface IParticleEffectProvider
{
    void Play(string ID);
    IParticleEffect GetParticleEffect(string ID);
}
