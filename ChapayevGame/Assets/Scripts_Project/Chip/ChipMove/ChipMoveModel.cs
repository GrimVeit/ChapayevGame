using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipMoveModel
{
    public event Action<ChipMove_Player> OnAddChip;
    public event Action<ChipMove_Player> OnRemoveChip;

    public void AddChip(ChipMove_Player chipMove)
    {
        OnAddChip?.Invoke(chipMove);
    }

    public void RemoveChip(ChipMove_Player chipMove)
    {
        OnRemoveChip?.Invoke(chipMove);
    }
}
