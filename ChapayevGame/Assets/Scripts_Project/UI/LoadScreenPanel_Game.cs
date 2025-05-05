using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScreenPanel_Game : MovablePanel
{
    [SerializeField] private AnimationFrame animationFrame;

    public override void ActivatePanel()
    {
        base.ActivatePanel();

        animationFrame.Activate(1);
    }

    public override void DeactivatePanel()
    {
        base.DeactivatePanel();

        animationFrame.Deactivate();
    }
}
