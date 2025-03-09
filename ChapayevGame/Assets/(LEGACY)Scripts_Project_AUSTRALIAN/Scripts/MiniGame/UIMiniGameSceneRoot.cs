using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMiniGameSceneRoot : MonoBehaviour
{
    [SerializeField] private MainPanel_Game mainPanel;
    [SerializeField] private SpinStartPanel_Game spinStartPanel;
    [SerializeField] private SpinPanel_Game spinPanel;
    [SerializeField] private LosePanel_Game losePanel;
    [SerializeField] private WinPanel_Game winPanel;

    private ISoundProvider soundProvider;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    private Panel currentPanel;

    public void Initialize()
    {
        mainPanel.Initialize();
        spinStartPanel.Initialize();
        spinPanel.Initialize();
        losePanel.Initialize();
        winPanel.Initialize();
    }

    public void Dispose()
    {
        mainPanel.Dispose();
        spinStartPanel.Dispose();
        spinPanel.Dispose();
        losePanel.Dispose();
        winPanel.Dispose();
    }



    public void OpenMainPanel()
    {
        if(mainPanel.IsActive) return;

        OpenPanel(mainPanel);
    }





    public void OpenSpinStartPanel()
    {
        OpenOtherPanel(spinStartPanel);
    }

    public void CloseSpinStartPanel()
    {
        CloseOtherPanel(spinStartPanel);
    }





    public void OpenSpinPanel()
    {
        OpenOtherPanel(spinPanel);
    }

    public void CloseSpinPanel()
    {
        CloseOtherPanel(spinPanel);
    }





    public void OpenLosePanel()
    {
        if(losePanel.IsActive) return;

        OpenOtherPanel(losePanel);
    }

    public void CloseLosePanel()
    {
        if (!losePanel.IsActive) return;

        CloseOtherPanel(losePanel);
    }





    public void OpenWinPanel()
    {
        if(winPanel.IsActive) return;

        OpenOtherPanel(winPanel);
    }

    public void CloseWinPanel()
    {
        if(!winPanel.IsActive) return;

        CloseOtherPanel(winPanel);
    }





    public void Activate()
    {
        OpenMainPanel();
    }

    public void Deactivate()
    {
        if (currentPanel != null)
           CloseOtherPanel(currentPanel);

        CloseWinPanel();
    }



    private void OpenPanel(Panel panel)
    {
        if (currentPanel != null)
            currentPanel.DeactivatePanel();

        currentPanel = panel;
        currentPanel.ActivatePanel();

    }

    private void OpenOtherPanel(Panel panel)
    {
        panel.ActivatePanel();
    }

    private void CloseOtherPanel(Panel panel)
    {
        panel.DeactivatePanel();
    }

    #region Input


    #endregion
}
