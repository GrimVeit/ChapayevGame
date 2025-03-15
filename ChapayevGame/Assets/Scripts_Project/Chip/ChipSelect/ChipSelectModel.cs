using System;
using UnityEngine;

public class ChipSelectModel
{
    public event Action<Chip> OnSetOpenChip;
    public event Action<Chip> OnSetOpenNewChip;
    public event Action<int> OnSetChipCount;
    public event Action<int> OnSelectChip;
    public event Action<int> OnDeselectChip;

    public event Action OnActivate;
    public event Action OnDeactivate;

    public event Action<int> OnChooseChip;

    private int countChip;
    private int currentCountChip = 0;

    private bool isActivate = false;

    private ITutorialDescriptionProvider tutorialDescriptionProvider;

    public ChipSelectModel(ITutorialDescriptionProvider tutorialDescriptionProvider)
    {
        this.tutorialDescriptionProvider = tutorialDescriptionProvider;
    }

    public void SetOpenChip(Chip chip)
    {
        OnSetOpenChip?.Invoke(chip);
    }

    public void SetOpenNewChip(Chip chip)
    {
        OnSetOpenNewChip?.Invoke(chip);
    }

    public void SelectChip(int id)
    {
        if (!isActivate) return;

        OnSelectChip?.Invoke(id);

        currentCountChip += 1;
        Check();
    }

    public void DeselectChip(int id)
    {
        if(!isActivate) return;

        OnDeselectChip?.Invoke(id);

        currentCountChip -= 1;
        Check();
    }

    public void SetCountChip(int count)
    {
        countChip = count;
        currentCountChip = 0;
        OnSetChipCount?.Invoke(count);
        isActivate = true;
    }



    public void ChooseChip(int id)
    {
        OnChooseChip?.Invoke(id);
    }

    private void Check()
    {
        if(countChip == currentCountChip)
        {
            tutorialDescriptionProvider.LockTutorial("ChooseChips");
            OnActivate?.Invoke();
        }
        else
        {
            OnDeactivate?.Invoke();
        }

        //Debug.Log(currentCountChip);
    }
}
