using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipMoveModel
{
    public event Action<ChipMove> OnAddChip;
    public event Action<ChipMove> OnRemoveChip;

    public void AddChip(ChipMove chipMove)
    {
        OnAddChip?.Invoke(chipMove);
    }

    public void RemoveChip(ChipMove chipMove)
    {
        OnRemoveChip?.Invoke(chipMove);
    }
}
