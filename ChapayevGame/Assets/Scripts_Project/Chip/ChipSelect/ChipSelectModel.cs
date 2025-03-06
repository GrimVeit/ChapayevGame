using System;
using UnityEngine;

public class ChipSelectModel
{
    public event Action<Chip> OnSetOpenChip;
    public event Action<int> OnSetChipCount;
    public event Action<int> OnSelectChip;
    public event Action<int> OnDeselectChip;

    public event Action OnActivate;
    public event Action OnDeactivate;

    public event Action<int> OnChooseChip;

    private int countChip;
    private int currentCountChip = 0;

    public void SetOpenChip(Chip chip)
    {
        OnSetOpenChip?.Invoke(chip);
    }

    public void SelectChip(int id)
    {
        OnSelectChip?.Invoke(id);

        currentCountChip += 1;
        Check();
    }

    public void DeselectChip(int id)
    {
        OnDeselectChip?.Invoke(id);

        currentCountChip -= 1;
        Check();
    }

    public void SetCountChip(int count)
    {
        countChip = count;
        OnSetChipCount?.Invoke(count);
    }



    public void ChooseChip(int id)
    {
        OnChooseChip?.Invoke(id);
    }

    private void Check()
    {
        if(countChip == currentCountChip)
        {
            OnActivate?.Invoke();
        }
        else
        {
            OnDeactivate?.Invoke();
        }

        Debug.Log(currentCountChip);
    }
}
