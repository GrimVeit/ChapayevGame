using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFramePresenter
{
    private readonly AnimationFrameModel model;
    private readonly AnimationFrameView view;

    public AnimationFramePresenter(AnimationFrameModel model, AnimationFrameView view)
    {
        this.model = model;
        this.view = view;
    }

    public void Initialize()
    {
        ActivateEvents();
    }

    public void Dispose()
    {
        DeactivateEvents();
    }

    private void ActivateEvents()
    {

    }

    private void DeactivateEvents()
    {

    }
}
