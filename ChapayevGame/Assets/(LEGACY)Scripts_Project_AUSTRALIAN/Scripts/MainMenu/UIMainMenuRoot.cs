using System;
using UnityEngine;

public class UIMainMenuRoot : MonoBehaviour
{
    [SerializeField] private MainPanel_Menu mainPanel;
    [SerializeField] private StoreChipPanel_Menu storeChipPanel;
    [SerializeField] private StoreStrategyPanel_Menu storeStrategyPanel;
    [SerializeField] private LoadBuyPanel_Menu loadBuyPanel;
    [SerializeField] private ChipPresentationPanel_Menu chipPresentationPanel;
    [SerializeField] private StrategyPresentationPanel_Menu strategyPresentationPanel;

    private ISoundProvider soundProvider;

    private Panel currentPanel;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public void Initialize()
    {
        mainPanel.Initialize();
        storeChipPanel.Initialize();
        storeStrategyPanel.Initialize();
        loadBuyPanel.Initialize();
        chipPresentationPanel.Initialize();
        strategyPresentationPanel.Initialize();
    }

    public void Activate()
    {
        OpenMainPanel();
    }


    public void Deactivate()
    {

    }

    public void Dispose()
    {
        mainPanel.Dispose();
        storeChipPanel.Dispose();
        storeStrategyPanel.Dispose();
        loadBuyPanel.Dispose();
        chipPresentationPanel.Dispose();
    }





    public void OpenMainPanel()
    {
        OpenPanel(mainPanel);
    }

    public void OpenStoreChipPanel()
    {
        OpenPanel(storeChipPanel);
    }

    public void OpenStoreStrategyPanel()
    {
        OpenPanel(storeStrategyPanel);
    }




    public void OpenLoadBuyPanel()
    {
        OpenOtherPanel(loadBuyPanel);
    }

    public void CloseLoadBuyPanel()
    {
        CloseOtherPanel(loadBuyPanel);
    }





    public void OpenChipPresentationPanel()
    {
        OpenOtherPanel(chipPresentationPanel);
    } 

    public void CloseChipPresentationPanel()
    {
        CloseOtherPanel(chipPresentationPanel);
    }




    public void OpenStrategyPresentationPanel()
    {
        OpenOtherPanel(strategyPresentationPanel);
    }

    public void CloseStartegyPresentationPanel()
    {
        CloseOtherPanel(strategyPresentationPanel);
    }





    #region Base

    private void OpenPanel(Panel panel)
    {
        if (currentPanel == panel) return;

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

    #endregion

    #region Input



    #region MainPanel

    public event Action OnClickToOpenBuyStrategy
    {
        add => mainPanel.OnClickToStrategy += value;
        remove => mainPanel.OnClickToStrategy -= value;
    }

    public event Action OnClickToOpenBuyChip
    {
        add => mainPanel.OnClickToCollection += value;
        remove => mainPanel.OnClickToCollection -= value;
    }

    public event Action OnClickToOpenChooseStrategy
    {
        add => mainPanel.OnClickToBattle += value;
        remove => mainPanel.OnClickToBattle -= value;
    }

    #endregion

    #region BuyStrategyPanel

    public event Action OnClickToBackFromBuyStrategy
    {
        add => storeStrategyPanel.OnClickToCancel += value;
        remove => storeStrategyPanel.OnClickToCancel -= value;
    }

    #endregion

    #region BuyStrategyPanel

    public event Action OnClickToBackFromBuyChip
    {
        add => storeChipPanel.OnClickToCancel += value;
        remove => storeChipPanel.OnClickToCancel -= value;
    }

    #endregion

    #endregion

}
