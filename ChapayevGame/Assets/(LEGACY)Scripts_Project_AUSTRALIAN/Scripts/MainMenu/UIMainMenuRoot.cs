using System;
using UnityEngine;

public class UIMainMenuRoot : MonoBehaviour
{
    [SerializeField] private MainPanel_Menu mainPanel;
    [SerializeField] private StoreChipPanel_Menu storeChipPanel;
    [SerializeField] private StoreStrategyPanel_Menu storeStrategyPanel;
    [SerializeField] private LoadBuyPanel_Menu loadBuyStrategyPanel;
    [SerializeField] private LoadBuyPanel_Menu loadBuyChipPanel;
    [SerializeField] private ChipPresentationPanel_Menu chipPresentationPanel;
    [SerializeField] private StrategyPresentationPanel_Menu strategyPresentationPanel;

    [SerializeField] private ChooseStrategyPanel_Menu chooseStrategyPanel;
    [SerializeField] private ChooseChipPanel_Menu chooseChipPanel;

    private ISoundProvider soundProvider;

    private Panel currentPanel;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public void Initialize()
    {
        mainPanel.SetSoundProvider(soundProvider);
        storeChipPanel.SetSoundProvider(soundProvider);
        storeStrategyPanel.SetSoundProvider(soundProvider);
        chooseStrategyPanel.SetSoundProvider(soundProvider);
        chooseChipPanel.SetSoundProvider(soundProvider);

        mainPanel.Initialize();
        storeChipPanel.Initialize();
        storeStrategyPanel.Initialize();
        loadBuyStrategyPanel.Initialize();
        chipPresentationPanel.Initialize();
        strategyPresentationPanel.Initialize();

        chooseStrategyPanel.Initialize();
        chooseChipPanel.Initialize();
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
        loadBuyStrategyPanel.Dispose();
        chipPresentationPanel.Dispose();

        chooseStrategyPanel.Dispose();
        chooseChipPanel.Dispose();
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

    public void OpenChooseStrategyPanel()
    {
        OpenPanel(chooseStrategyPanel);
    }

    public void OpenChooseChipPanel()
    {
        OpenPanel(chooseChipPanel);
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

    public void CloseStrategyPresentationPanel()
    {
        CloseOtherPanel(strategyPresentationPanel);
    }




    public void OpenLoadBuyStrategyPanel()
    {
        OpenOtherPanel(loadBuyStrategyPanel);
    }

    public void CloseLoadBuyStrategyPanel()
    {
        CloseOtherPanel(loadBuyStrategyPanel);
    }





    public void OpenLoadBuyChipPanel()
    {
        OpenOtherPanel(loadBuyChipPanel);
    }

    public void CloseLoadBuyChipPanel()
    {
        CloseOtherPanel(loadBuyChipPanel);
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
