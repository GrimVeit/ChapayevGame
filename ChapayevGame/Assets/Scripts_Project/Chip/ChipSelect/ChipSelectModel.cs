using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipSelectModel
{
    public event Action<Chip> OnSetOpenChip;
    public event Action<int> OnSelectChip;
    public event Action<int> OnDeselectChip;

    public event Action<int> OnChooseChip;

    public void SetOpenChip(Chip chip)
    {
        OnSetOpenChip?.Invoke(chip);
    }

    public void SelectChip(int id)
    {
        OnSelectChip?.Invoke(id);
    }

    public void DeselectChip(int id)
    {
        OnDeselectChip?.Invoke(id);
    }



    public void ChooseChip(int id)
    {
        OnChooseChip?.Invoke(id);
    }
}
