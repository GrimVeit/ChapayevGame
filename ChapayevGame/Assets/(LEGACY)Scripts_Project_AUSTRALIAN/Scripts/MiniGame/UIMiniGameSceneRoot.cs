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

    [SerializeField] private StoreChipPanel_Game storeChipPanel;
    [SerializeField] private StoreStrategyPanel_Game storeStrategyPanel;
    [SerializeField] private ChooseStrategyPanel_Game chooseStrategyPanel;
    [SerializeField] private ChooseChipPanel_Game chooseChipPanel;

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

        storeChipPanel.Initialize();
        storeStrategyPanel.Initialize();
        chooseStrategyPanel.Initialize();
        chooseChipPanel.Initialize();
    }

    public void Dispose()
    {
        mainPanel.Dispose();
        spinStartPanel.Dispose();
        spinPanel.Dispose();
        losePanel.Dispose();
        winPanel.Dispose();

        storeChipPanel.Dispose();
        storeStrategyPanel.Dispose();
        chooseStrategyPanel.Dispose();
        chooseChipPanel.Dispose();
    }



    public void OpenMainPanel()
    {
        if(mainPanel.IsActive) return;

        OpenPanel(mainPanel);
    }

    public void OpenWinPanel()
    {
        if (winPanel.IsActive) return;

        OpenPanel(winPanel);
    }

    public void OpenLosePanel()
    {
        if (losePanel.IsActive) return;

        OpenPanel(losePanel);
    }

    public void OpenStoreChipPanel()
    {
        OpenPanel(storeChipPanel);
    }

    public void OpenStoreStrategyPanel()
    {
        OpenPanel(storeStrategyPanel);
    }

    public void OpenChooseStrategyPanel()
    {
        OpenPanel(chooseStrategyPanel);
    }

    public void OpenChooseChipPanel()
    {
        OpenPanel(chooseChipPanel);
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





    public void Activate()
    {
        OpenMainPanel();
    }

    public void Deactivate()
    {
        if (currentPanel != null)
           CloseOtherPanel(currentPanel);
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


    #region WinPanel

    public event Action OnClickToOpenBuyStrategy_WinPanel
    {
        add => winPanel.OnClickToOpenStrategyStore += value;
        remove => winPanel.OnClickToOpenStrategyStore -= value;
    }

    public event Action OnClickToOpenBuyChip_WinPanel
    {
        add => winPanel.OnClickToOpenChipStore += value;
        remove => winPanel.OnClickToOpenChipStore -= value;
    }

    public event Action OnClickToOpenChooseStrategy_WinPanel
    {
        add => winPanel.OnClickToChooseStrategy += value;
        remove => winPanel.OnClickToChooseStrategy -= value;
    }

    #endregion

    #region LosePanel

    public event Action OnClickToOpenBuyStrategy_LosePanel
    {
        add => losePanel.OnClickToOpenStrategyStore += value;
        remove => losePanel.OnClickToOpenStrategyStore -= value;
    }

    public event Action OnClickToOpenBuyChip_LosePanel
    {
        add => losePanel.OnClickToOpenChipStore += value;
        remove => losePanel.OnClickToOpenChipStore -= value;
    }

    public event Action OnClickToOpenChooseStrategy_LosePanel
    {
        add => losePanel.OnClickToChooseStrategy += value;
        remove => losePanel.OnClickToChooseStrategy -= value;
    }

    #endregion



    #region BuyStrategyPanel

    public event Action OnClickToBackFromBuyStrategy
    {
        add => storeStrategyPanel.OnClickToCancel += value;
        remove => storeStrategyPanel.OnClickToCancel -= value;
    }

    #endregion

    #region BuyChipPanel

    public event Action OnClickToBackFromBuyChip
    {
        add => storeChipPanel.OnClickToCancel += value;
        remove => storeChipPanel.OnClickToCancel -= value;
    }

    #endregion

    #region ChooseStrategyPanel

    public event Action OnClickToOpenChooseChipFromChooseStrategy
    {
        add => chooseStrategyPanel.OnClickToContinue += value;
        remove => chooseStrategyPanel.OnClickToContinue -= value;
    }

    public event Action OnClickToCancelFromChooseStrategy
    {
        add => chooseStrategyPanel.OnClickToCancel += value;
        remove => chooseStrategyPanel.OnClickToCancel -= value;
    }

    #endregion

    #region ChooseChipPanel

    public event Action OnClickToOpenChooseStrategyFromChooseChip
    {
        add => chooseChipPanel.OnClickToCancel += value;
        remove => chooseChipPanel.OnClickToCancel -= value;
    }

    public event Action OnClickToPlay
    {
        add => chooseChipPanel.OnClickToPlay += value;
        remove => chooseChipPanel.OnClickToPlay -= value;
    }

    #endregion

    #endregion
}
